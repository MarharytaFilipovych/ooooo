using RecipeManager.ActionRegistry;
using RecipeManager.Actions;
using RecipeManager.Commands;
using RecipeManager.Commands.ActionCommands;
using RecipeManager.Storage;

namespace RecipeManager.Executors.ActionExecutors;

public class ActionsExecutor(IRecipeStorage recipeStorage, IIngredientStorage ingredientStorage,
    IActionRegistry actionRegistry) : ICommandExecutor<ActionCommand>
{
    public ExecuteResult Execute(ActionCommand command)
    {
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