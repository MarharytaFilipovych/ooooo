using RecipeManager.CommandDefinitions.StockDefinitions;
using RecipeManager.Executors.StockExecutors;
using RecipeManager.Storage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public class StockCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager, IPlanValidator planValidator) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new StockAddDefinition(), new StockAddExecutor(userStorage, storageManager, planValidator));
        context.Register(new StockInfoDefinition(), new StockInfoExecutor(userStorage, storageManager));
        context.Register(new StockUseDefinition(), new StockUseExecutor(userStorage, storageManager));
    }
}