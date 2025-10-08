namespace RecipeManager.Events;

public class PlanChangedEvent : IEvent
{
    public string EventName => "plan_changed";
    public DateTime Timestamp { get; }
    public string UserName { get; }
    public string PlanName { get; }

    public PlanChangedEvent(string userName, string planName)
    {
        UserName = userName;
        PlanName = planName;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "user_name", UserName },
            { "plan_name", PlanName }
        };
    }
}