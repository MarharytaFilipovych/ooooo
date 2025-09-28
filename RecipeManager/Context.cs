using System.Numerics;
using RecipeManager.CommandDefinitions;
using RecipeManager.Commands;
using RecipeManager.Executors;

namespace RecipeManager;

public class Context
{
    private static readonly Dictionary<string, ICommandDefinition> CommandDefinitions = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<Type, ITypedExecutor> Executors = new();
    
    private interface ITypedExecutor
    {
        ExecuteResult Execute(ICommand command);
    }

    private sealed class TypedExecutor<TCommand>(ICommandExecutor<TCommand> inner) :
        ITypedExecutor where TCommand : ICommand
    {
        public ExecuteResult Execute(ICommand command) =>  inner.Execute((TCommand) command);
    }


    public void Register<TCommand>(ICommandDefinition commandDefinition, ICommandExecutor<TCommand> executor)
        where TCommand : ICommand
    {
        CommandDefinitions[commandDefinition.Name] = commandDefinition;
        Executors[typeof(TCommand)] = new TypedExecutor<TCommand>(executor);
    }

    public List<ICommandDefinition> Commands => CommandDefinitions.Values.ToList();


    public ExecuteResult GetExecutionResult(string[] args, out string? error)
    {
        error = null;
        var executeResult = ExecuteResult.Error;

        if (args.Length == 0 )
        {
            error = "Where is your input???";
            return executeResult;
        }
        
        if (!Enum.TryParse(args[0], out CommandGroup group))
        {
            error = $"Unrecognized command! You can use one of: {string.Join(", ", Enum.GetNames<CommandGroup>())}";
            return executeResult;
        }
        
        var commandName = group.ToString().ToLower();
        if (args.Length > 1) commandName += args[1].ToLower();
        

        if (!CommandDefinitions.TryGetValue(commandName, out var definition))
        {
            error = "Hmmm, we don't know such a command!!!";
            return executeResult;
        }

        var parseResult = definition.Parse(args);

        if (parseResult.Error != null)
        {
            error = parseResult.Error;
            return executeResult;
        }

        var commandType = parseResult.GetType();

        if (!Executors.TryGetValue(commandType, out var executor))
        {
            error = $"There is no any register executor for this command {commandType.Name}";
            return executeResult;
        }
        
        return executor.Execute(parseResult.Command!);
    }
}