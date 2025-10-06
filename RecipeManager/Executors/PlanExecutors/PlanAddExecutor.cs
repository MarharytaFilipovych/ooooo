using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.Executors.PlanExecutors;

public class PlanAddExecutor(ISessionStorage sessionStorage, 
    IStorage<Plan> planStorage, IPlanValidator planValidator)
    : ICommandExecutor<PlanAddCommand>
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
            return ExecuteResult.Continue;
        }

        var plan = new Plan(command.Name, command.RecipeName, command.Date, command.ServingsMultiplier);
        Console.WriteLine(
            planStorage.Add(plan)
                ? $"Plan \"{command.Name}\" was successfully added!"
                : $"Plan with a name \"{command.Name}\" has already been added!"
        );
        return ExecuteResult.Continue;
    }
}