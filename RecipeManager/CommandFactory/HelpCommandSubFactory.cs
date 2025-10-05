using RecipeManager.CommandDefinitions.HelpDefinitions;
using RecipeManager.Executors.HelpExecutors;

namespace RecipeManager.CommandFactory;

public class HelpCommandSubFactory : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new HelpDefinition(), new HelpExecutor(context.Commands));
    }
}