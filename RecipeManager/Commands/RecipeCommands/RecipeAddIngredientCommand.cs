using RecipeManager.Entities;

namespace RecipeManager.Commands.RecipeCommands;

public class RecipeAddIngredientCommand(string name, string ingredientName, 
    decimal quantity, Unit unit) : RecipeWithNameCommand(name)
{
    public string IngredientName { get; } = ingredientName;
    public decimal Quantity { get; } = quantity;
    public Unit Unit { get; } = unit;
}