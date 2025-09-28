using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.PlanExecutors;

public class PlanAddExecutor(IStorage<Plan> storage) : ICommandExecutor<PlanAddCommand>
{
    public ExecuteResult Execute(PlanAddCommand command)
    {
        var plan = new Plan(command.Name, command.RecipeName, command.Date, command.ServingsMultiplier);
        Console.WriteLine(
            storage.Add(plan)
                ? $"Plan {plan} was successfully added!"
                : $"Plan with a name {command.Name} has already been added!"
        );
        return ExecuteResult.Continue;
    }
}