namespace RecipeManager.Events;

public class RecipeCookedEvent : IEvent
{
    public string EventName => "recipe_cooked";
    public DateTime Timestamp { get; }
    public string RecipeName { get; }
    public decimal Servings { get; }

    public RecipeCookedEvent(string recipeName, decimal servings)
    {
        RecipeName = recipeName;
        Servings = servings;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "recipe_name", RecipeName },
            { "servings", Servings }
        };
    }
}