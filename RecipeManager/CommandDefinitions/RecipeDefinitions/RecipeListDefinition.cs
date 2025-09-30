using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;

namespace RecipeManager.CommandDefinitions.RecipeDefinitions;

public class RecipeListDefinition : ICommandDefinition
{
    public string Name => "recipe list";
    public string Description => "recipe list";
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = new RecipeListCommand();
        return true;
    }
}