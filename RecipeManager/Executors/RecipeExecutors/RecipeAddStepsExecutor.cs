using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddStepsExecutor(IUserStorage userStorage, UserStorageManager storageManager) : ICommandExecutor<CommandAddSteps>
{
    public ExecuteResult Execute(CommandAddSteps command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var recipeStorage = storageManager.GetRecipeStorage(currentUser.Username);
        var recipe = recipeStorage.GetEntityByName(command.Name);
        if (recipe == null)
        {
            Console.WriteLine($"Recipe with a name \"{command.Name}\" " +
                              $"was not found! No steps added.");
            return ExecuteResult.Continue;
        }
        recipe.AddSteps(command.StepText);
        Console.WriteLine($"Step \"{command.StepText}\" was successfully added to this great recipe!");
        return ExecuteResult.Continue;
    }
}