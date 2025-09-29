using RecipeManager.CommandDefinitions;
using RecipeManager.Commands;
using RecipeManager.Commands.HelpCommands;

namespace RecipeManager.Executors.HelpExecutors;

public class HelpExecutor(List<ICommandDefinition> commands) :
    ICommandExecutor<HelpCommand>
{
    public ExecuteResult Execute(HelpCommand command)
    {
        Console.WriteLine("Here is your precious help:");
        commands.ForEach(c =>
            Console.WriteLine($"- {c.Description}"));
        return ExecuteResult.Continue;
    }
}