using RecipeManager.Commands;

namespace RecipeManager.CommandFactory;

public interface ICommandFactory
{
    void Register(ICommand command);
    List<ICommand> ListCommands();
}