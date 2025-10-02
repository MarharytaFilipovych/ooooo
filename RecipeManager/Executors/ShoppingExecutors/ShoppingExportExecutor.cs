using RecipeManager.Commands;
using RecipeManager.Commands.ShoppingCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.ShoppingExecutors;

public class ShoppingExportExecutor(IUserStorage userStorage, UserStorageManager storageManager)
    : ICommandExecutor<ShoppingExportCommand>
{
    public ExecuteResult Execute(ShoppingExportCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var planStorage = storageManager.GetPlanStorage(currentUser.Username);
        var plans = planStorage.GetAll();
        var recipeMap = GetRecipeMap(plans);
        
        PrintShoppingList(plans, recipeMap);
        
        return ExecuteResult.Continue;
    }

    private Dictionary<string, Recipe> GetRecipeMap(List<Plan> plans)
    {
        var currentUser = userStorage.GetCurrentUser();
        var recipeStorage = storageManager.GetRecipeStorage(currentUser.Username);
        var recipeNames = plans.Select(plan => plan.RecipeName);
        var recipes = recipeStorage.GetRecipesByNames(recipeNames);
        return recipes.ToDictionary(r => r.Name, StringComparer.OrdinalIgnoreCase);
    }

    private static void PrintShoppingList(List<Plan> plans, Dictionary<string, Recipe> recipes)
    {
        Console.WriteLine("Shopping List:");
    
        var shoppingList = plans
            .Where(plan => recipes.ContainsKey(plan.RecipeName))
            .SelectMany(plan => recipes[plan.RecipeName].Ingredients
                .Select(ingredient => new
                {
                    ingredient.Name,
                    Quantity = ingredient.Quantity * plan.ServingsMultiplier,
                    ingredient.Unit
                }))
            .GroupBy(x => (x.Name.ToLowerInvariant(), x.Unit))
            .Select(group => new
            {
                Name = group.Key.Item1,
                TotalQuantity = group.Sum(x => x.Quantity),
                Unit = group.Key.Item2
            })
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Unit)
            .ToList();
    
        if (shoppingList.Count == 0) 
            Console.WriteLine("No ingredients needed because you don't have any plans!");
        else 
            shoppingList.ForEach(item => 
                Console.WriteLine($"- \"{item.Name}\": {item.TotalQuantity} {item.Unit}"));
    }
}