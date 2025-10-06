using RecipeManager.CommandDefinitions.ChangePlanDefinitions;
using RecipeManager.CommandDefinitions.PlanDefinitions;
using RecipeManager.Executors.ChangePlanExecutors;
using RecipeManager.Executors.PlanExecutors;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public class ChangePlanCommandSubFactory(ISessionStorage sessionStorage,
    IPlanValidator planValidator) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ChangePlanDefinition(), new ChangePlanExecutor(sessionStorage, planValidator));
    }
}