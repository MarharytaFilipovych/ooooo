using RecipeManager.Entities;

namespace RecipeManager.Commands.StockCommands;

public class StockUseCommand(string ingredientName, decimal quantity,
    Unit unit, string? reason) : ICommand

{
    public string IngredientName { get; } = ingredientName;
    public decimal Quantity { get; } = quantity;
    public Unit Unit { get; } = unit;
    public string? Reason { get; } = null;
}