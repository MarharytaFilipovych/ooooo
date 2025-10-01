using RecipeManager.ActionRegistry;
using RecipeManager.Actions;
using RecipeManager.Commands;
using RecipeManager.Commands.ActionCommands;
using RecipeManager.Storage;

namespace RecipeManager.Executors.ActionExecutors;

public class ActionsExecutor(IUserStorage userStorage, UserStorageManager storageManager,
    IActionRegistry actionRegistry) : ICommandExecutor<ActionCommand>
{
    public ExecuteResult Execute(ActionCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var recipeStorage = storageManager.GetRecipeStorage(currentUser.Username);
        var ingredientStorage = storageManager.GetIngredientStorage(currentUser.Username);
        var recipe = recipeStorage.GetEntityByName(command.Name);
        if (recipe == null)
        {
            Console.WriteLine($"Recipe \"{command.Name}\" not found!");
            return ExecuteResult.Continue;
        }

        var action = actionRegistry.GetAction(command.ActionName);
        if (action == null)
        {
            Console.WriteLine($"Action \"{command.ActionName}\" does not exist!");
            return ExecuteResult.Continue;
        }

        var context = new ActionContext(
            recipe,
            ingredientStorage,
            recipeStorage,
            command.Parameter
        );
        var result = action.Execute(context);
        
        Console.WriteLine(result.Message);

        return ExecuteResult.Continue;
    }
}