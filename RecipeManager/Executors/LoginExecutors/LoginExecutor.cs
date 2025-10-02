using RecipeManager.Commands;
using RecipeManager.Commands.LoginCommands;
using RecipeManager.Storage;

namespace RecipeManager.Executors.LoginExecutors;

public class LoginExecutor(IUserStorage userStorage, UserStorageManager storageManager) : ICommandExecutor<LoginCommand>
{
    public ExecuteResult Execute(LoginCommand command)
    {
        var user = userStorage.GetOrCreateUser(command.Username);
        userStorage.SetCurrentUser(user);

        Console.WriteLine($"Logged in as {user.Username}");
        return ExecuteResult.Continue;
    }
}