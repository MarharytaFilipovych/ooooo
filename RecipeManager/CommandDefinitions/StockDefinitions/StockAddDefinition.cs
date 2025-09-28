using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;

namespace RecipeManager.CommandDefinitions.StockDefinitions;

public class StockAddDefinition : ICommandDefinition
{
    public string Name => "stock add";
    public string Description => "stock add \"<ingredient>\" <quantity> <unit>";

    public ParseResult Parse(string[] args)
    {
        var result = new ParseResult();
        if (args.Length != 5)
        {
            result.Error = $"Wrong usage, bro: {Description}";
            return result;
        }

        var ingredientName = args[2];
        
        if (!decimal.TryParse(args[3], out var quantity) || quantity < 0)
        {
            result.Error = $"The quantity was supposed to be numeric and greater than 0" +
                           $", not you strange value: {args[3]}";
        }

        if (!Enum.TryParse(args[4], out Unit unit))
        {
            result.Error = $"Supported units: {string.Join(", ", Enum.GetNames(typeof(Unit)))}";
        }

        result.Command = new StockAddCommand(ingredientName, quantity, unit);
        return result;
    }
}