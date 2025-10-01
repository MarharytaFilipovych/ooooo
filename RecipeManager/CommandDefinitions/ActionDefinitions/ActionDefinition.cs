using RecipeManager.Commands;
using RecipeManager.Commands.ActionCommands;

namespace RecipeManager.CommandDefinitions.ActionDefinitions;

public class ActionDefinition : ICommandDefinition
{
    public string Name => "action";
    public string Description => "action <action_name> \"<recipe_name>\" [<parameters>]";

    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;

        if (args.Length < 3)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var actionName = args[1];
        var recipeName = args[2];
        string? parameter = null;

        if (args.Length > 3)
        {
            parameter = args[3];
        }

        if (string.IsNullOrWhiteSpace(actionName))
        {
            error = "Action name cannot be empty!";
            return false;
        }

        if (string.IsNullOrWhiteSpace(recipeName))
        {
            error = "Recipe name cannot be empty!";
            return false;
        }

        command = new ActionCommand(actionName, recipeName, parameter);
        return true;
    }
}