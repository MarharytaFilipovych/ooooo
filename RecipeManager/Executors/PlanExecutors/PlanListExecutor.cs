using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.PlanExecutors;

public class PlanListExecutor(IStorage<Plan> storage) : ICommandExecutor<PlanListCommand>
{
    public ExecuteResult Execute(PlanListCommand command)
    {
        storage.GetAll().ForEach(Console.WriteLine);
        return ExecuteResult.Continue;
    }
}