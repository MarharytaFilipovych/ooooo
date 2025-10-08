using RecipeManager.Entities;

namespace RecipeManager.Events;

public class StockAddedEvent : IEvent
{
    public string EventName => "stock_added";
    public DateTime Timestamp { get; }
    public string Ingredient { get; }
    public decimal Quantity { get; }
    public Unit Unit { get; }

    public StockAddedEvent(string ingredient, decimal quantity, Unit unit)
    {
        Ingredient = ingredient;
        Quantity = quantity;
        Unit = unit;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "ingredient", Ingredient },
            { "quantity", Quantity },
            { "unit", Unit.ToString() }
        };
    }
}