using RecipeManager.Commands;

namespace RecipeManager.CommandFactory;

public class CommandFactory : ICommandFactory
{
    public void Register(ICommand command)
    {
        throw new NotImplementedException();
    }

    public List<ICommand> ListCommands()
    {
        throw new NotImplementedException();
    }
}