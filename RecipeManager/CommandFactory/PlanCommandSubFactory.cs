using RecipeManager.CommandDefinitions.PlanDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.PlanExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class PlanCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new PlanAddDefinition(), new PlanAddExecutor(userStorage, storageManager));
        context.Register(new PlanListDefinition(), new PlanListExecutor(userStorage, storageManager));
    }
}