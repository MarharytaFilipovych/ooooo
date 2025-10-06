using RecipeManager.CommandDefinitions;
using RecipeManager.Commands;
using RecipeManager.Executors;
using RecipeManager.Storage.SessionStorage;

namespace RecipeManager;

public class Context
{
    private readonly Dictionary<string, ICommandDefinition> _commandDefinitions =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<Type, ITypedExecutor> _executors = new();
    private ISessionStorage? _sessionStorage;
    
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

    public void SetUserStorage(ISessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public List<ICommandDefinition> Commands => _commandDefinitions.Values.ToList();


    public ExecuteResult GetExecutionResult(string[] args, out string? error)
    {
        error = null;
        var executeResult = ExecuteResult.Error;

        if (!TryValidateArgs(args, out error) || 
            !TryParseCommandGroup(args[0], out var group, out error) || 
            !ValidateUserLoggedIn(group, out error)) return executeResult;

        var commandName = BuildCommandName(group, args);

        if (!TryGetCommandDefinition(commandName, out var definition, out error) 
            || !definition!.TryParse(args, out var command, out error)) return executeResult;

        var commandType = command!.GetType();
        if (_executors.TryGetValue(commandType, out var executor))
            return executor.Execute(command);

        error = $"There is no any registered executor for this command {commandType.Name}";
        return executeResult;
    }

    private static bool TryValidateArgs(string[] args, out string? error)
    {
        if (args.Length == 0)
        {
            error = "Where is your input???";
            return false;
        }

        error = null;
        return true;
    }
    
    private static bool TryParseCommandGroup(string arg, out CommandGroup group, out string? error)
    {
        if (!Enum.TryParse(arg.Replace("_", ""), ignoreCase: true, out group))
        {
            error = $"Unrecognized command! You can use one of: {string.Join(", ", Enum.GetNames<CommandGroup>())}";
            return false;
        }

        error = null;
        return true;
    }
    
    private bool ValidateUserLoggedIn(CommandGroup group, out string? error)
    {
        if (CommandRequiresUserToBeLoggedIn(group) &&
            (_sessionStorage == null || !_sessionStorage.CurrentUserIsLoaded()))
        {
            error = "You must login first! Use: login <username> or get out!";
            return false;
        }

        error = null;
        return true;
    }
    
    private string BuildCommandName(CommandGroup group, string[] args)
    {
        var commandName = string.Concat(
            group.ToString().Select((c, i) => 
                i > 0 && char.IsUpper(c) ? "_" + char.ToLower(c) : char.ToLower(c).ToString()
            )
        );

        var singleWordCommands = new[]
        {
            CommandGroup.Options, CommandGroup.Action,
            CommandGroup.Help, CommandGroup.Exit,
            CommandGroup.Login, CommandGroup.ChangePlan
        };

        if (!singleWordCommands.Contains(group) && args.Length > 1) commandName += " " + args[1].ToLower();

        return commandName;
    }

    private bool TryGetCommandDefinition(string commandName, out ICommandDefinition? definition, out string? error)
    {
        if (!_commandDefinitions.TryGetValue(commandName, out definition))
        {
            error = "Hmmm, we don't know such a command!!!";
            return false;
        }

        error = null;
        return true;
    }

    private bool CommandRequiresUserToBeLoggedIn(CommandGroup group) =>
        group != CommandGroup.Login && group != CommandGroup.Help && group != CommandGroup.Exit;

}