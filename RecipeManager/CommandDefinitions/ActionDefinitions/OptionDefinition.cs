using RecipeManager.Commands;
using RecipeManager.Commands.ActionCommands;

namespace RecipeManager.CommandDefinitions.ActionDefinitions;

public class OptionDefinition : ICommandDefinition
{
    public string Name => "options";
    public string Description => "options \"<recipe_name>\"";
    
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;

        if (args.Length != 2)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var recipeName = args[1];

        if (string.IsNullOrWhiteSpace(recipeName))
        {
            error = "Recipe name cannot be empty!";
            return false;
        }

        command = new OptionsCommand(recipeName);
        return true;
    }
}