using RecipeManager.Entities;

namespace RecipeManager.Commands.StockCommands;

public class StockAddCommand(string ingredientName,
    decimal quantity, Unit unit) : 
    StockWithIngredientDetailsCommand(ingredientName, quantity, unit) { }