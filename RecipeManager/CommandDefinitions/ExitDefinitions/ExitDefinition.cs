using RecipeManager.Commands;
using RecipeManager.Commands.ExitCommands;

namespace RecipeManager.CommandDefinitions.ExitDefinitions;

public class ExitDefinition : ICommandDefinition
{
    public string Name => "exit";
    public string Description => "exit";

    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = new ExitCommand();
        return true;
    }
}