using RecipeManager.CommandDefinitions.StockDefinitions;
using RecipeManager.Executors.StockExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class StockCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new StockAddDefinition(), new StockAddExecutor(userStorage, storageManager));
        context.Register(new StockInfoDefinition(), new StockInfoExecutor(userStorage, storageManager));
        context.Register(new StockUseDefinition(), new StockUseExecutor(userStorage, storageManager));
    }
}