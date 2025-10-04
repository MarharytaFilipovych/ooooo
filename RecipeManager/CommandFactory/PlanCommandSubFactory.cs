using RecipeManager.CommandDefinitions.PlanDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.PlanExecutors;
using RecipeManager.Storage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public class PlanCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager, IPlanValidator planValidator, ISubscriptionStorage subscriptionStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new PlanAddDefinition(), new PlanAddExecutor(userStorage, storageManager, planValidator));
        context.Register(new PlanListDefinition(), new PlanListExecutor(userStorage, storageManager));
        context.Register(new ChangePlanDefinition(), new ChangePlanExecutor(userStorage, subscriptionStorage, planValidator));
    }
}