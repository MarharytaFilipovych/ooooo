using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;

namespace RecipeManager.CommandDefinitions.StockDefinitions;

public class StockAddDefinition : ICommandDefinition
{
    public string Name => "stock add";
    public string Description => "stock add \"<ingredient>\" <quantity> <unit>";

    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;
        if (args.Length != 5)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var ingredientName = args[2];
        
        if (!decimal.TryParse(args[3], out var quantity) || quantity < 0)
        {
            error = $"The quantity was supposed to be numeric and greater than 0" +
                           $", not you strange value: {args[3]}";
            return false;
        }

        if (!Enum.TryParse(args[4], ignoreCase: true, out Unit unit))
        {
            error = $"Supported units: {string.Join(", ", Enum.GetNames(typeof(Unit)))}";
            return false;
        }

        command = new StockAddCommand(ingredientName, quantity, unit);
        return true;
    }
}