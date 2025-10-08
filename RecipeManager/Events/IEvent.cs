namespace RecipeManager.Events;

public interface IEvent
{
    string EventName { get; }
    DateTime Timestamp { get; }
    Dictionary<string, object> GetParams();
}