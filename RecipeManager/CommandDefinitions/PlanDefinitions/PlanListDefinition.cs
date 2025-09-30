using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;

namespace RecipeManager.CommandDefinitions.PlanDefinitions;

public class PlanListDefinition : ICommandDefinition
{
    public string Name => "plan list";
    public string Description => "plan list";
    
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = new PlanListCommand();
        return true;
        
    }
}