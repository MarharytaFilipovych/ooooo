using RecipeManager.Commands.BaseCommands;
using RecipeManager.Entities;

namespace RecipeManager.Commands.RecipeCommands;

public class CommandAddIngredient(string name, string ingredientName, 
    decimal quantity, Unit unit) : CommandWithFirstArgName(name)
{
    public string IngredientName { get; } = ingredientName;
    public decimal Quantity { get; } = quantity;
    public Unit Unit { get; } = unit;
}