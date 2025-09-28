using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;

namespace RecipeManager.CommandDefinitions.RecipeDefinitions;

public class RecipeAddStepsDefinition : ICommandDefinition
{
    public string Name => "recipe steps";
    public string Description => "recipe steps add \"<recipe_name>\" \"<step_text>\"";
    
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;
        if (args.Length != 5)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var name = args[3];
        var steps = args[4];

        command = new CommandAddSteps(name, steps);
        return true;
    }
}