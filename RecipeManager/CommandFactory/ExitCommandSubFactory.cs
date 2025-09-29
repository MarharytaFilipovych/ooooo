using RecipeManager.CommandDefinitions.ExitDefinitions;
using RecipeManager.Executors.ExitExecutors;

namespace RecipeManager.CommandFactory;

public class ExitCommandSubFactory : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ExitDefinition(), new ExitExecutor());
    }
}