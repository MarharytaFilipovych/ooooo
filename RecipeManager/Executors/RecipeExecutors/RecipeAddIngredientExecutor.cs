using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddIngredientExecutor(IStorage<Recipe> recipeStorage) 
    : ICommandExecutor<CommandAddIngredient>
{
    public ExecuteResult Execute(CommandAddIngredient command)
    {
        var recipe = recipeStorage.GetEntityByName(command.Name);
        
        if (recipe == null)
        {
            Console.WriteLine($"Recipe with a name \"{command.Name}\" " +
                              $"was not found! No ingredients added.");
            return ExecuteResult.Continue;
        }

        if (recipe.Ingredients.Any(i => i.Name == command.IngredientName))
        {
            Console.WriteLine($"Ingredient with a name \"{command.IngredientName}\" " +
                              $"has been already added to this great recipe! You cannot have ingredient with " +
                              $"same names within one recipe, sweety:)!");
            return ExecuteResult.Continue;
        }
        
        recipe.AddIngredient(new Ingredient(command.IngredientName, command.Quantity, command.Unit));
        Console.WriteLine($"Ingredient with a name \"{command.IngredientName}\" " +
                          $"was successfully added to this great recipe!");
        return ExecuteResult.Continue;
    }
}