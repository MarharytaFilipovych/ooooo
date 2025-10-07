using RecipeManager.Commands;
using RecipeManager.Commands.LoginCommands;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.EventPublishing;
using RecipeManager.Events;

namespace RecipeManager.Executors.LoginExecutors;

public class LoginExecutor(ISessionStorage sessionStorage, IEventPublisher eventPublisher) : ICommandExecutor<LoginCommand>
{
    public ExecuteResult Execute(LoginCommand command)
    {
        if (sessionStorage.CurrentUserIsLoaded()) sessionStorage.Save();

        var success = sessionStorage.TryLoad(command.Username, out var user, out var error);
        
        if (success)
        {
            eventPublisher.Publish(new UserLoggedInEvent(user!.Username));
            Console.WriteLine($"Logged in as {user.Username} with {user.Subscription.Type} plan");
        }
        else
        {
            Console.WriteLine(error);
        }

        return ExecuteResult.Continue;
    }
}