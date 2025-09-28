using RecipeManager.Commands;
using RecipeManager.Commands.HelpCommands;

namespace RecipeManager.CommandDefinitions.HelpDefinitions;

public class HelpDefinition : ICommandDefinition
{
    public string Name => "help";
    public string Description => "help";
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = new HelpCommand();
        return true;
    }    
}