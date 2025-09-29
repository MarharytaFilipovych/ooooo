using RecipeManager.Commands;
using RecipeManager.Commands.ShoppingCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.ShoppingExecutors;

public class ShoppingExportExecutor(IStorage<Plan> planStorage, IRecipeStorage recipeStorage,
    IIngredientStorage ingredientStorage) : ICommandExecutor<ShoppingExportCommand>
{
    public ExecuteResult Execute(ShoppingExportCommand command)
    {
        var plans = planStorage.GetAll();
        var recipeMap = GetRecipeMap(plans);
        var ingredientMap = GetIngredientMap(plans, recipeMap);
        var shoppingList = GenerateShoppingList(plans, recipeMap, ingredientMap);
        
        PrintShoppingList(shoppingList);
        
        return ExecuteResult.Continue;
    }

    private Dictionary<string, Recipe> GetRecipeMap(List<Plan> plans)
    {
        var recipeNames = plans.Select(plan => plan.RecipeName);
        var recipes = recipeStorage.GetRecipesByNames(recipeNames);
        return recipes.ToDictionary(r => r.Name, StringComparer.OrdinalIgnoreCase);
    }

    private Dictionary<string, Ingredient> GetIngredientMap(List<Plan> plans, Dictionary<string, Recipe> recipeLookup)
    {
        var allIngredientNames = plans
            .Where(plan => recipeLookup.ContainsKey(plan.RecipeName))
            .SelectMany(plan => recipeLookup[plan.RecipeName].Ingredients)
            .Distinct(StringComparer.OrdinalIgnoreCase);
        
        return ingredientStorage
            .GetIngredientsByNames(allIngredientNames)
            .ToDictionary(i => i.Name, StringComparer.OrdinalIgnoreCase);
    }

    private List<ShoppingListItem> GenerateShoppingList(List<Plan> plans, 
        Dictionary<string, Recipe> recipeLookup, 
        Dictionary<string, Ingredient> ingredientLookup)
    {
        return plans
            .Where(plan => recipeLookup.ContainsKey(plan.RecipeName))
            .SelectMany(plan => GetPlanIngredients(plan, recipeLookup[plan.RecipeName], ingredientLookup))
            .GroupBy(x => x.IngredientName, StringComparer.OrdinalIgnoreCase)
            .Select(group => new ShoppingListItem
            {
                IngredientName  = group.Key,
                TotalQuantity = group.Sum(x => x.TotalQuantity),
                Unit = group.First().Unit
            })
            .OrderBy(x => x.IngredientName )
            .ToList();
    }

    private static IEnumerable<ShoppingListItem> GetPlanIngredients(
        Plan plan, Recipe recipe, Dictionary<string, Ingredient> ingredientMap)
    {
        return recipe.Ingredients
            .Where(ingredientMap.ContainsKey)
            .Select(ingredientName => 
            {
                var ingredient = ingredientMap[ingredientName];
                return new ShoppingListItem
                {
                    IngredientName = ingredientName,
                    TotalQuantity = ingredient.Quantity * plan.ServingsMultiplier,
                    Unit = ingredient.Unit.ToString()
                };
            });
    }

    private static void PrintShoppingList(List<ShoppingListItem> shoppingList)
    {
        Console.WriteLine("Shopping List:");
        
        if (shoppingList.Count == 0) Console.WriteLine("No ingredients needed because you don't have any plans!.");
        else shoppingList.ForEach(item => 
                Console.WriteLine($"- \"{item.IngredientName }\": {item.TotalQuantity} {item.Unit}"));
    }

    private record ShoppingListItem
    {
        public string? IngredientName  { get; init; } = string.Empty;
        public decimal TotalQuantity { get; init; }
        public string Unit { get; init; } = string.Empty;
    }
}