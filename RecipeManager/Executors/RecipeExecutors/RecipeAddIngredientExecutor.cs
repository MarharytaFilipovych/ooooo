using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddIngredientExecutor(IStorage<Recipe> recipeStorage, IStorage<Ingredient> ingredientStorage) 
    : ICommandExecutor<CommandAddIngredient>
{
    public ExecuteResult Execute(CommandAddIngredient command)
    {
        var recipe = recipeStorage.GetEntityByName(command.Name);
        if (recipe == null)
        {
            Console.WriteLine($"Recipe with a name {command.Name} " +
                              $"was not found! No ingredients added.");
            return ExecuteResult.Continue;
        }

        if (!ingredientStorage.ExistsByName(command.IngredientName))
        {
            Console.WriteLine($"Ingredient with a name {command.IngredientName} " +
                              $"was not found! No ingredients added.");
            return ExecuteResult.Continue;
        }
        
        recipe.AddIngredient(command.IngredientName);
        return ExecuteResult.Continue;
    }
}