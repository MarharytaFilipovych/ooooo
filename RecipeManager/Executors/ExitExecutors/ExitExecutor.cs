using RecipeManager.Commands;
using RecipeManager.Commands.ExitCommands;
using RecipeManager.Storage.SessionStorage;

namespace RecipeManager.Executors.ExitExecutors;

public class ExitExecutor(ISessionStorage sessionStorage) : ICommandExecutor<ExitCommand>
{
    public ExecuteResult Execute(ExitCommand command)
    {
        Console.WriteLine("Exiting, my love....");
        if(sessionStorage.CurrentUserIsLoaded())sessionStorage.Save();
        return ExecuteResult.Break;
    }
}