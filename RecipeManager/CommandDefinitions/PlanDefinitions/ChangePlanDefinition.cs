using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;

namespace RecipeManager.CommandDefinitions.PlanDefinitions;

public class ChangePlanDefinition : ICommandDefinition
{
    public string Name => "changePlan";
    public string Description => "changePlan <plan_name>";

    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;

        if (args.Length != 2)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var planName = args[1];
        
        if (!Enum.TryParse(planName, ignoreCase: true, out PlanType planType))
        {
            error = $"Unknown plan type. Available plans: {string.Join(", ", Enum.GetNames<PlanType>())}";
            return false;
        }

        command = new ChangePlanCommand(planType);
        return true;
    }
}