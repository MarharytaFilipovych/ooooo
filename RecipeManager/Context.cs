using System.Numerics;
using RecipeManager.CommandDefinitions;
using RecipeManager.Commands;
using RecipeManager.Executors;
using RecipeManager.Storage;

namespace RecipeManager;

public class Context
{
    private readonly Dictionary<string, ICommandDefinition> _commandDefinitions = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<Type, ITypedExecutor> _executors = new();
    private IUserStorage? _userStorage;
    
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
        _commandDefinitions[commandDefinition.Name] = commandDefinition;
        _executors[typeof(TCommand)] = new TypedExecutor<TCommand>(executor);
    }

    public void SetUserStorage(IUserStorage userStorage)
    {
        _userStorage = userStorage;
    }

    public List<ICommandDefinition> Commands => _commandDefinitions.Values.ToList();


    public ExecuteResult GetExecutionResult(string[] args, out string? error)
    {
        error = null;
        var executeResult = ExecuteResult.Error;

        if (args.Length == 0 )
        {
            error = "Where is your input???";
            return executeResult;
        }
        
        if (!Enum.TryParse(args[0], ignoreCase: true, out CommandGroup group))
        {
            error = $"Unrecognized command! You can use one of: {string.Join(", ", Enum.GetNames<CommandGroup>())}";
            return executeResult;
        }

        if (group != CommandGroup.Login && group != CommandGroup.Help && group != CommandGroup.Exit)
        {
            if (_userStorage == null || !_userStorage.HasCurrentUser())
            {
                error = "You must login first! Use: login <username>";
                return executeResult;
            }
        }
        
        var commandName = group.ToString().ToLower();

        var singleWordCommands = new[]
            { CommandGroup.Options, CommandGroup.Action, CommandGroup.Help, CommandGroup.Exit, CommandGroup.Login, CommandGroup.ChangePlan };

        if (!singleWordCommands.Contains(group) && args.Length > 1)
        {
            commandName += " " + args[1].ToLower();
        }
        
        if (!_commandDefinitions.TryGetValue(commandName, out var definition))
        {
            error = "Hmmm, we don't know such a command!!!";
            return executeResult;
        }


        if (!definition.TryParse(args, out var command, out var parsingError))
        {
            error = parsingError;
            return executeResult;
        }


        var commandType = command!.GetType();

        if (_executors.TryGetValue(commandType, out var executor)) return executor.Execute(command);
        error = $"There is no any register executor for this command {commandType.Name}";
        return executeResult;

    }
}