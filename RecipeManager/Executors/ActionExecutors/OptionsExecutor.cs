using System.Text;
using RecipeManager.ActionRegistry;
using RecipeManager.Commands;
using RecipeManager.Commands.ActionCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.ActionExecutors;

public class OptionsExecutor(
    IStorage<Recipe> recipeStorage, IActionRegistry actionRegistry)
    : ICommandExecutor<OptionsCommand>
{
    public ExecuteResult Execute(OptionsCommand command)
    {
        var recipe = recipeStorage.GetEntityByName(command.Name);

        if (recipe == null)
        {
            Console.WriteLine($"Recipe \"{command.Name}\" not found!");
            return ExecuteResult.Continue;
        }

        var actions = actionRegistry.GetAllActions();
        var result = new StringBuilder();

        foreach (var a in actions.OrderBy(a => a.Name))
        {
            var param = a.RequiresParam ? " [parameter]" : "";
            result.AppendLine($"  * {a.Name}{param}: {a.Description}");
        }

        result.AppendLine("\nUsage: action <action_name> \"" + recipe.Name + "\" [parameters]");
        
        Console.WriteLine(result.ToString());
        return ExecuteResult.Continue;
    }
}