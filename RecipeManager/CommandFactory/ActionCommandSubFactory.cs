using RecipeManager.ActionRegistry;
using RecipeManager.Actions;
using RecipeManager.CommandDefinitions.ActionDefinitions;
using RecipeManager.Executors.ActionExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class ActionCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager)
    : ICommandSubFactory
{
    public void Create(Context context)
    {
        var actionRegistry = new RecipeActionRegistry();
        
        actionRegistry.Register(new InfoAction());
        actionRegistry.Register(new ScaleAction());
        actionRegistry.Register(new MissingAction());
        actionRegistry.Register(new CookAction());
        
        context.Register(
            new OptionDefinition(),
            new OptionsExecutor(userStorage, storageManager, actionRegistry)
            );
        
        context.Register(
            new ActionDefinition(),
            new ActionsExecutor(userStorage, storageManager, actionRegistry)
            );
    }
    
}