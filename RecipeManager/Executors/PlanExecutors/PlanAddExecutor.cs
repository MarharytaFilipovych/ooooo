using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;
using RecipeManager.Events;

namespace RecipeManager.Executors.PlanExecutors;

public class PlanAddExecutor(
    ISessionStorage sessionStorage, 
    IStorage<Plan> planStorage, 
    IPlanValidator planValidator,
    IEventPublisher eventPublisher) : ICommandExecutor<PlanAddCommand>
{
    public ExecuteResult Execute(PlanAddCommand command)
    {
        if (!sessionStorage.TryGetCurrentUser(out var currentUser))
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }
        
        var validationResult = planValidator.CanAddPlan(currentUser!.Subscription);
        if (!validationResult.IsValid)
        {
            Console.WriteLine(validationResult.ErrorMessage);
            eventPublisher.Publish(new LimitReachedEvent("plans"));
            return ExecuteResult.Continue;
        }

        var plan = new Plan(command.Name, command.RecipeName, command.Date, command.ServingsMultiplier);
        
        var added = planStorage.Add(plan);
        if (added)
        {
            eventPublisher.Publish(new PlannedAddedEvent(command.RecipeName, command.Date, command.ServingsMultiplier ?? 1));
            
            Console.WriteLine($"Plan \"{command.Name}\" was successfully added!");
        }
        else
        {
            Console.WriteLine($"Plan with a name \"{command.Name}\" has already been added!");
        }
        
        return ExecuteResult.Continue;
    }
}