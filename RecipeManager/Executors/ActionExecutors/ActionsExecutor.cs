using RecipeManager.ActionRegistry;
using RecipeManager.Actions;
using RecipeManager.Commands;
using RecipeManager.Commands.ActionCommands;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.EventPublishing;
using RecipeManager.Events;

namespace RecipeManager.Executors.ActionExecutors;

public class ActionsExecutor(
    IRecipeStorage recipeStorage,
    IIngredientStorage ingredientStorage, 
    IActionRegistry actionRegistry,
    IEventPublisher eventPublisher) : ICommandExecutor<ActionCommand>
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
        
        if (command.ActionName.Equals("cook", StringComparison.OrdinalIgnoreCase) && result.Success)
        {
            decimal multiplier = 1;
            if (!string.IsNullOrWhiteSpace(command.Parameter))
            {
                decimal.TryParse(command.Parameter, out multiplier);
            }
            eventPublisher.Publish(new RecipeCookedEvent(recipe.Name, multiplier));
        }

        return ExecuteResult.Continue;
    }
}