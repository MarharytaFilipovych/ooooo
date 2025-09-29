using RecipeManager.CommandDefinitions.PlanDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.PlanExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class PlanCommandSubFactory(IStorage<Plan> planStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new PlanAddDefinition(), new PlanAddExecutor(planStorage));
        context.Register(new PlanListDefinition(), new PlanListExecutor(planStorage));
    }
}