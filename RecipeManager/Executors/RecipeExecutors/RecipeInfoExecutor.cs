using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeInfoExecutor(IUserStorage userStorage, UserStorageManager storageManager) : ICommandExecutor<CommandInfo>
{
    public ExecuteResult Execute(CommandInfo command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var recipeStorage = storageManager.GetRecipeStorage(currentUser.Username);
        var recipe = recipeStorage.GetEntityByName(command.Name);
        Console.WriteLine(recipe?.ToString() 
                          ?? $"Recipe with a name \"{command.Name}\" was not " +
                          $"found! No ingredients added.");
        
        return ExecuteResult.Continue;
      
    }
}