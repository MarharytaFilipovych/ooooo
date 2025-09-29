using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;

namespace RecipeManager.CommandDefinitions.RecipeDefinitions;

public class RecipeAddIngredientDefinition : ICommandDefinition
{
    public string Name => "recipe add-ingredient";
    public string Description => "recipe add-ingredient \"<recipe_name>\" \"<ingredient>\" <quantity> <unit>";
   
    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;
        if (args.Length != 6)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var name = args[2];
        var ingredientName = args[3];
        if (!decimal.TryParse(args[4], out var quantity) || quantity < 0)
        {
            error = $"The quantity was supposed to be numeric and greater than 0" +
                           $", not you strange value: {args[3]}";
            return false;
        }

        if (!Enum.TryParse(args[5], ignoreCase: true, out Unit unit))
        {
            error = $"Supported units: {string.Join(", ", Enum.GetNames(typeof(Unit)))}";
            return false;
        }
        
        command = new CommandAddIngredient(name, ingredientName, quantity, unit);
        return true;
        
    }
}