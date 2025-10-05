using RecipeManager.CommandDefinitions.LoginDefinitions;
using RecipeManager.Executors.LoginExecutors;
using RecipeManager.Storage.SessionStorage;

namespace RecipeManager.CommandFactory;

public class LoginCommandSubFactory(ISessionStorage sessionStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new LoginDefinition(), new LoginExecutor(sessionStorage));
    }
}