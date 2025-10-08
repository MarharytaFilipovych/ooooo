namespace RecipeManager.Events;

public class LimitReachedEvent : IEvent
{
    public string EventName => "limit_reached";
    public DateTime Timestamp { get; }
    public string LimitType { get; }

    public LimitReachedEvent(string limitType)
    {
        LimitType = limitType;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "limit_type", LimitType }
        };
    }
}