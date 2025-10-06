using RecipeManager.CommandDefinitions.ExitDefinitions;
using RecipeManager.Executors.ExitExecutors;
using RecipeManager.Storage.SessionStorage;

namespace RecipeManager.CommandFactory;

public class ExitCommandSubFactory(ISessionStorage sessionStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ExitDefinition(), new ExitExecutor(sessionStorage));
    }
}