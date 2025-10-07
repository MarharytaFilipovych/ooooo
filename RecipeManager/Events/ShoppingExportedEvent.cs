namespace RecipeManager.Events;

public class ShoppingExportedEvent : IEvent
{
    public string EventName => "shopping_exported";
    public DateTime Timestamp { get; }
    public int ItemsCount { get; }

    public ShoppingExportedEvent(int itemsCount)
    {
        ItemsCount = itemsCount;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "items_count", ItemsCount }
        };
    }
}