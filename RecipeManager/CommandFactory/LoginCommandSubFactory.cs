using RecipeManager.CommandDefinitions.LoginDefinitions;
using RecipeManager.Executors.LoginExecutors;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.EventPublishing;

namespace RecipeManager.CommandFactory;

public class LoginCommandSubFactory(ISessionStorage sessionStorage, IEventPublisher eventPublisher) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new LoginDefinition(), new LoginExecutor(sessionStorage, eventPublisher));
    }
}