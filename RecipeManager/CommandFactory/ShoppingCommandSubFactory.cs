using RecipeManager.CommandDefinitions.ShoppingDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.ShoppingExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class ShoppingCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ShoppingExportDefinition(),
            new ShoppingExportExecutor(userStorage, storageManager));
    }
}