using RecipeManager.Commands;

namespace RecipeManager.Executors;

public interface ICommandExecutor<TCommand> where TCommand: ICommand
{
    ExecuteResult Execute(TCommand command);
}