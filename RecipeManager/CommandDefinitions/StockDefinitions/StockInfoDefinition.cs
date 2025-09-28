using RecipeManager.Commands.StockCommands;

namespace RecipeManager.CommandDefinitions.StockDefinitions;

public class StockInfoDefinition : ICommandDefinition
{
    public string Name => "stock info";
    public string Description => "stock info";
    public ParseResult Parse(string[] args) => new(new StockInfoCommand());
}