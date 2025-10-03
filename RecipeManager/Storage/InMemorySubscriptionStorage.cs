using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemorySubscriptionStorage : ISubscriptionStorage
{
    private readonly Dictionary<string, UserSubscription> _subscriptions = 
        new(StringComparer.OrdinalIgnoreCase);

    public UserSubscription? GetSubscription(string username)
    {
        _subscriptions.TryGetValue(username, out var subscription);
        return subscription;
    }

    public void CreateSubscription(string username, SubscriptionPlan plan)
    {
        if (HasSubscription(username))
            throw new InvalidOperationException($"Subscription for {username} already exists");

        _subscriptions[username] = new UserSubscription(username, plan);
    }

    public void UpdateSubscription(UserSubscription subscription)
    {
        if (subscription == null)
            throw new ArgumentNullException(nameof(subscription));

        _subscriptions[subscription.Username] = subscription;
    }

    public bool HasSubscription(string username)
    {
        return _subscriptions.ContainsKey(username);
    }
}