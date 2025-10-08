using RecipeManager.CommandDefinitions.PlanDefinitions;
using RecipeManager.Executors.PlanExecutors;
using RecipeManager.Storage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;
using RecipeManager.Entities;

namespace RecipeManager.CommandFactory;

public class PlanCommandSubFactory(ISessionStorage sessionStorage, IStorage<Plan> planStorage, IPlanValidator planValidator, IEventPublisher eventPublisher) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new PlanAddDefinition(), new PlanAddExecutor(sessionStorage, planStorage, planValidator, eventPublisher));
        context.Register(new PlanListDefinition(), new PlanListExecutor(planStorage));
    }
}