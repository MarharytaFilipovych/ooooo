using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;

namespace RecipeManager.CommandDefinitions.RecipeDefinitions;

public class RecipeInfoDefinition : ICommandDefinition
{
    public string Name => "recipe info";
    public string Description => "recipe info \"<recipe_name>\"";
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;
        if (args.Length != 3)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var name = args[2];

        command = new RecipeInfoCommand(name);
        return true;
    }
}