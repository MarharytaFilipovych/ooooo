using RecipeManager.Commands.ExitCommands;

namespace RecipeManager.Executors.ExitExecutors;

public class ExitExecutor : ICommandExecutor<ExitCommand>
{
    public ExecuteResult Execute(ExitCommand command)
    {
        Console.WriteLine("Exiting, my love....");
        return ExecuteResult.Break;
    }
}