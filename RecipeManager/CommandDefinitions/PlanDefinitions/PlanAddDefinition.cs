using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;

namespace RecipeManager.CommandDefinitions.PlanDefinitions;

public class PlanAddDefinition : ICommandDefinition
{
    public string Name => "plan add";
    public string Description => "plan add \"<name>\" \"<recipe_name>\" <date> [<servings_multiplier>]";
    
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;
        
        command = null;
        if (args.Length < 5)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var name = args[2];
        var recipeName = args[3];

        if (!DateTime.TryParse(args[4], out var date))
        {
            error = "Your date was in a strange format!";
            return false;
        }

        
        decimal servingsMultiplier = 1;

        if (args.Length > 4 && !decimal.TryParse(args[5], out servingsMultiplier))
        {
            error = "Servings multiplier has to be of a decimal value!";
            return false;
        }

        
        command = new PlanAddCommand(name, recipeName, date, servingsMultiplier);
        return true;
        
    }
}