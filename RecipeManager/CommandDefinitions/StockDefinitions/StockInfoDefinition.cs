using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;

namespace RecipeManager.CommandDefinitions.StockDefinitions;

public class StockInfoDefinition : ICommandDefinition
{
    public string Name => "stock info";
    public string Description => "stock info";

    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = new StockInfoCommand();
        return true;
    }
}