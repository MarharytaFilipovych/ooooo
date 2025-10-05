using RecipeManager.Factories;

namespace RecipeManager.Entities;

public class User
{
    public string Username { get; }
    public SubscriptionPlan Subscription { get; set; }

    public User(string username, SubscriptionPlan subscription)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Recipe name cannot be empty");
        Subscription = subscription;
        Username = username.Trim();
    }
    
    public User(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Recipe name cannot be empty");
        Subscription = SubscriptionPlanFactory.CreateBasicPlan();
        Username = username.Trim();
    }

    public override string ToString()
    {
        return $"User: {Username}";
    }
}