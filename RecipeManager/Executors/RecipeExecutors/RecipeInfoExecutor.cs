using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeInfoExecutor(IStorage<Recipe> recipeStorage) : ICommandExecutor<CommandInfo>
{
    public ExecuteResult Execute(CommandInfo command)
    {
        var recipe = recipeStorage.GetEntityByName(command.Name);
        Console.WriteLine(recipe?.ToString() 
                          ?? $"Recipe with a name \"{command.Name}\" was not " +
                          $"found! No ingredients added.");
        
        return ExecuteResult.Continue;
      
    }
}