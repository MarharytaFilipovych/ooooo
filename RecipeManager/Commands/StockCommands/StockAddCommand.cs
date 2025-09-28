using RecipeManager.Entities;

namespace RecipeManager.Commands.StockCommands;

public class StockAddCommand(string ingredientName,
    decimal quantity, Unit unit) : ICommand

{
    public string IngredientName { get; } = ingredientName;
    public decimal Quantity { get; } = quantity;
    public Unit Unit { get; } = unit;
}