using RecipeManager.Entities;

namespace RecipeManager.Events;

public class RecipeIngredientEvent : IEvent
{
    public string EventName => "recipe_ingredient";
    public DateTime Timestamp { get; }
    public string RecipeName { get; }
    public string Ingredient { get; }
    public decimal Quantity { get; }
    public Unit Unit { get; }

    public RecipeIngredientEvent(string recipeName, string ingredient, decimal quantity, Unit unit)
    {
        RecipeName = recipeName;
        Ingredient = ingredient;
        Quantity = quantity;
        Unit = unit;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "recipe_name", RecipeName },
            { "ingredient", Ingredient },
            { "quantity", Quantity },
            { "unit", Unit.ToString() }
        };
    }
}