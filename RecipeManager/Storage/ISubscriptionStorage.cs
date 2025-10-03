using RecipeManager.Entities;

namespace RecipeManager.Storage;

public interface ISubscriptionStorage
{
    UserSubscription? GetSubscription(string username);
    void CreateSubscription(string username, SubscriptionPlan plan);
    void UpdateSubscription(UserSubscription subscription);
    bool HasSubscription(string username);
}