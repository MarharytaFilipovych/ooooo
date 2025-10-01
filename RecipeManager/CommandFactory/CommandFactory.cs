using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public static class CommandFactory
{
    public static Context Create()
    {
        var userStorage = new InMemoryUserStorage();
        var storageManager = new UserStorageManager();
        
        var context = new Context();
        context.SetUserStorage(userStorage);
        
        var subfactories = new ICommandSubFactory[]
        {
            new LoginCommandSubFactory(userStorage, storageManager),
            new StockCommandSubFactory(userStorage, storageManager),
            new RecipeCommandSubFactory(userStorage, storageManager),
            new PlanCommandSubFactory(userStorage, storageManager),
            new ShoppingCommandSubFactory(userStorage, storageManager),
            new ActionCommandSubFactory(userStorage, storageManager),
            new HelpCommandSubFactory(),
            new ExitCommandSubFactory()
        };
        
        foreach (var subfactory in subfactories)
        {
            subfactory.Create(context);
        }
        
        return context;
    }
}