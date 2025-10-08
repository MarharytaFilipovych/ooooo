using RecipeManager.Commands;
using RecipeManager.Commands.ChangePlanCommands;
using RecipeManager.Factories;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.Executors.ChangePlanExecutors;

public class ChangePlanExecutor(
    ISessionStorage sessionStorage,
    IPlanValidator planValidator) : ICommandExecutor<ChangePlanCommand>
{
    public ExecuteResult Execute(ChangePlanCommand command)
    {
        if (!sessionStorage.TryGetCurrentUser(out var currentUser))
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }
        
        if (currentUser!.Subscription.Type == command.NewPlanType)
        {
            Console.WriteLine("You are already on this plan!");
            return ExecuteResult.Continue;
        }

        var validationResult = planValidator.CanChangePlan(command.NewPlanType);
        if (!validationResult.IsValid)
        {
            Console.WriteLine(validationResult.ErrorMessage);
            return ExecuteResult.Continue;
        }

        var newPlan = SubscriptionPlanFactory.CreatePlan(command.NewPlanType);
        currentUser.Subscription = newPlan;
        
        Console.WriteLine($"Successfully changed to {command.NewPlanType} plan!");
        Console.WriteLine($"Now you have available: Pantry - {newPlan.MaxPantryItems}, \n" +
                          $"Recipes - {newPlan.MaxRecipes}, \nEntries - {newPlan.MaxPossibleEntries}.");
        return ExecuteResult.Continue;
    }
}