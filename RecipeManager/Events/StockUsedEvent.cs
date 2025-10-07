using RecipeManager.Entities;

namespace RecipeManager.Events;

public class StockUsedEvent : IEvent
{
    public string EventName => "stock_used";
    public DateTime Timestamp { get; }
    public string Ingredient { get; }
    public decimal Quantity { get; }
    public Unit Unit { get; }
    public string? Reason { get; }

    public StockUsedEvent(string ingredient, decimal quantity, Unit unit, string? reason)
    {
        Ingredient = ingredient;
        Quantity = quantity;
        Unit = unit;
        Reason = reason;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        var param = new Dictionary<string, object>
        {
            { "ingredient", Ingredient },
            { "quantity", Quantity },
            { "unit", Unit.ToString() }
        };

        if (!string.IsNullOrWhiteSpace(Reason))
        {
            param["reason"] = Reason;
        }

        return param;
    }
}