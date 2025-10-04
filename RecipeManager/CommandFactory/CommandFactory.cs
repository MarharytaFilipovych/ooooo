using RecipeManager.Storage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public static class CommandFactory
{
    public static Context Create()
    {
        var userStorage = new InMemoryUserStorage();
        var storageManager = new UserStorageManager();
        var subscriptionStorage = new InMemorySubscriptionStorage();
        
        var planValidator = new PlanValidator(userStorage, subscriptionStorage, storageManager);
        
        var context = new Context();
        context.SetUserStorage(userStorage);
        
        var subfactories = new ICommandSubFactory[]
        {
            new LoginCommandSubFactory(userStorage, storageManager, subscriptionStorage),
            new StockCommandSubFactory(userStorage, storageManager, planValidator),
            new RecipeCommandSubFactory(userStorage, storageManager, planValidator),
            new PlanCommandSubFactory(userStorage, storageManager, planValidator, subscriptionStorage),
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