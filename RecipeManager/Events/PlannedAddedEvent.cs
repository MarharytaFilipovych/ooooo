namespace RecipeManager.Events;

public class PlannedAddedEvent : IEvent
{
    public string EventName => "planned_added";
    public DateTime Timestamp { get; }
    public string RecipeName { get; }
    public DateTime Date { get; }
    public decimal Servings { get; }

    public PlannedAddedEvent(string recipeName, DateTime date, decimal servings)
    {
        RecipeName = recipeName;
        Date = date;
        Servings = servings;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "recipe_name", RecipeName },
            { "date", Date.ToString("yy-MM-dd") },
            { "servings", Servings }
        };
    }
}