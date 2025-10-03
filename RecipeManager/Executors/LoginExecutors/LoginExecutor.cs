using RecipeManager.Factories;
using RecipeManager.Commands;
using RecipeManager.Commands.LoginCommands;
using RecipeManager.Storage;

namespace RecipeManager.Executors.LoginExecutors;

public class LoginExecutor(IUserStorage userStorage, UserStorageManager storageManager, ISubscriptionStorage subscriptionStorage) : ICommandExecutor<LoginCommand>
{
    public ExecuteResult Execute(LoginCommand command)
    {
        var user = userStorage.GetOrCreateUser(command.Username);
        userStorage.SetCurrentUser(user);

        if (!subscriptionStorage.HasSubscription(user.Username))
        {
            var defaultPlan = SubscriptionPlanFactory.CreateBasicPlan();
            subscriptionStorage.CreateSubscription(user.Username, defaultPlan);
            Console.WriteLine($"Logged in as {user.Username} with {defaultPlan.Type} plan");
        }
        else
        {
            var subscription = subscriptionStorage.GetSubscription(user.Username);
            Console.WriteLine($"Logged in as {user.Username} with {subscription?.Plan.Type} plan");
        }
        return ExecuteResult.Continue;
    }
}