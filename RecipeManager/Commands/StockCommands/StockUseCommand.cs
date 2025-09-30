using RecipeManager.Entities;

namespace RecipeManager.Commands.StockCommands;

public class StockUseCommand(string ingredientName, decimal quantity,
    Unit unit, string? reason) : StockWithIngredientDetailsCommand(ingredientName, quantity, unit)
{
    public string? Reason { get; } = reason;
}