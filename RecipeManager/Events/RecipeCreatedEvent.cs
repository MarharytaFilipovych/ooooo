namespace RecipeManager.Events;

public class RecipeCreatedEvent : IEvent
{
    public string EventName => "recipe_created";
    public DateTime Timestamp { get; }
    public string RecipeName { get; }

    public RecipeCreatedEvent(string recipeName)
    {
        RecipeName = recipeName;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "recipe_name", RecipeName }
        };
    }
}