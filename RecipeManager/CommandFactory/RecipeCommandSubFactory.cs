using RecipeManager.CommandDefinitions.RecipeDefinitions;
using RecipeManager.Executors.RecipeExecutors;
using RecipeManager.Storage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public class RecipeCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager, IPlanValidator planValidator) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new RecipeAddDefinition(), new RecipeAddExecutor(userStorage, storageManager, planValidator));
        context.Register(new RecipeAddIngredientDefinition(), new RecipeAddIngredientExecutor(userStorage, storageManager));
        context.Register(new RecipeAddStepsDefinition(), new RecipeAddStepsExecutor(userStorage, storageManager));
        context.Register(new RecipeInfoDefinition(), new RecipeInfoExecutor(userStorage, storageManager));
        context.Register(new RecipeListDefinition(), new RecipeListExecutor(userStorage, storageManager));
    }
}