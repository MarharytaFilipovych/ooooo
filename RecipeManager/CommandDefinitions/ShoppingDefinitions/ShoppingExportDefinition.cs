using RecipeManager.Commands;
using RecipeManager.Commands.ShoppingCommands;

namespace RecipeManager.CommandDefinitions.ShoppingDefinitions;

public class ShoppingExportDefinition : ICommandDefinition
{
    public string Name => "shopping export";
    public string Description => "shopping export";
    
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = new ShoppingExportCommand();
        return true;
        
    }
}