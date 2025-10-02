using RecipeManager.CommandDefinitions.LoginDefinitions;
using RecipeManager.Executors.LoginExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class LoginCommandSubFactory(IUserStorage userStorage, UserStorageManager storageManager) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new LoginDefinition(), new LoginExecutor(userStorage, storageManager));
    }
}