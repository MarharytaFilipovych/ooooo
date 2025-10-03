namespace RecipeManager.Entities;

public class UserSubscription
{
    public string Username { get; }
    public SubscriptionPlan Plan { get; private set; }

    public UserSubscription(string username, SubscriptionPlan plan)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty!");

        Username = username;
        Plan = plan ?? throw new ArgumentNullException(nameof(plan));
    }

    public void ChangePlan(SubscriptionPlan newPlan)
    {
        Plan = newPlan ?? throw new ArgumentNullException(nameof(newPlan));
    }
    
    public override string ToString()
    {
        return $"User: {Username} - {Plan}";
    }
}