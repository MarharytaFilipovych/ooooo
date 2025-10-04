using RecipeManager.Factories;
using RecipeManager.Commands.PlanCommands;
using RecipeManager.Storage;
using RecipeManager.Utils;
using RecipeManager.Commands;

namespace RecipeManager.Executors.PlanExecutors;

public class ChangePlanExecutor(IUserStorage userStorage, ISubscriptionStorage subscriptionStorage,
    IPlanValidator planValidator) : ICommandExecutor<ChangePlanCommand>
{
    public ExecuteResult Execute(ChangePlanCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var subscription = subscriptionStorage.GetSubscription(currentUser.Username);
        if (subscription == null)
        {
            Console.WriteLine("Subscription not found. What the hell?");
            return ExecuteResult.Continue;
        }

        if (subscription.Plan.Type == command.NewPlanType)
        {
            Console.WriteLine("You are already on this plan!");
            return ExecuteResult.Continue;
        }

        var validationResult = planValidator.CanChangePlan(currentUser.Username, command.NewPlanType);
        if (!validationResult.IsValid)
        {
            Console.WriteLine(validationResult.ErrorMessage);
            return ExecuteResult.Continue;
        }

        var newPlan = SubscriptionPlanFactory.CreatePlan(command.NewPlanType);
        subscription.ChangePlan(newPlan);
        subscriptionStorage.UpdateSubscription(subscription);
        Console.WriteLine($"Successfully changed to {command.NewPlanType} plan!");
        Console.WriteLine($"Now you have available: Pantry - {newPlan.MaxPantryItems}, \n" +
                          $"Recipes - {newPlan.MaxRecipes}, \nEntries - {newPlan.MaxPossibleEntries}.");
        return ExecuteResult.Continue;
    }
}