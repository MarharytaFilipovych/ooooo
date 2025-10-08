namespace RecipeManager.Events;

public class UserLoggedInEvent : IEvent
{
    public string EventName => "user_logged_in";
    public DateTime Timestamp { get; }
    public string UserName { get; }

    public UserLoggedInEvent(string userName)
    {
        UserName = userName;
        Timestamp = DateTime.UtcNow;
    }

    public Dictionary<string, object> GetParams()
    {
        return new Dictionary<string, object>
        {
            { "user_name", UserName }
        };
    }
}