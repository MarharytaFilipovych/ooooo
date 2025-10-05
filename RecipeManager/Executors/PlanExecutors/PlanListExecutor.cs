using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.PlanExecutors;

public class PlanListExecutor(IStorage<Plan> planStorage) : ICommandExecutor<PlanListCommand>
{
    public ExecuteResult Execute(PlanListCommand command)
    {

        var plans = planStorage.GetAll();
        
        if (plans.Count == 0)
        {
            Console.WriteLine("No meal plans found. Add one with: plan add \"<name>\" \"<recipe_name>\" <date>");
            return ExecuteResult.Continue;
        }
        
        var sortedPlans = plans.OrderBy(p => p.Date).ToList();
        
        Console.WriteLine($"Found {sortedPlans.Count} meal plan(s):\n");
        
        for (var i = 0; i < sortedPlans.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {sortedPlans[i]}");
        }
        
        return ExecuteResult.Continue;
    }
}