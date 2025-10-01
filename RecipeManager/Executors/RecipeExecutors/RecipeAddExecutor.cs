using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddExecutor(IUserStorage userStorage, UserStorageManager storageManager) :
    ICommandExecutor<CommandAdd>
{
    public ExecuteResult Execute(CommandAdd command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var recipeStorage = storageManager.GetRecipeStorage(currentUser.Username);

        var recipe = new Recipe(command.Name);
        
        Console.WriteLine(recipeStorage.Add(recipe)
            ? $"The recipe \"{command.Name}\" was successfully added!"
            : "The recipe with such a name already exists!");

        return ExecuteResult.Continue;
    }
}