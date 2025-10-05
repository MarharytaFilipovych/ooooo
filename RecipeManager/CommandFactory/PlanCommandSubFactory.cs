using RecipeManager.CommandDefinitions.PlanDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.PlanExecutors;
using RecipeManager.Storage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public class PlanCommandSubFactory(ISessionStorage sessionStorage, 
    IStorage<Plan> planStorage, IPlanValidator planValidator) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new PlanAddDefinition(), new PlanAddExecutor(sessionStorage, planStorage, planValidator));
        context.Register(new PlanListDefinition(), new PlanListExecutor(planStorage));
    }
}