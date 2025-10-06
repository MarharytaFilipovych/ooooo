using RecipeManager.Commands;
using RecipeManager.Commands.LoginCommands;
using RecipeManager.Storage.SessionStorage;

namespace RecipeManager.Executors.LoginExecutors;

public class LoginExecutor(ISessionStorage sessionStorage) : ICommandExecutor<LoginCommand>
{
    public ExecuteResult Execute(LoginCommand command)
    {
        if (sessionStorage.CurrentUserIsLoaded()) sessionStorage.Save();

        Console.WriteLine(sessionStorage.TryLoad(command.Username, out var user, out var error)
            ? $"Logged in as {user!.Username} with {user.Subscription.Type} plan"
            : error);

        return ExecuteResult.Continue;
    }
}