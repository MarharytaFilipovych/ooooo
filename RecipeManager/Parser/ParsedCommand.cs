using RecipeManager.Commands;

namespace RecipeManager.Parser;

public class ParsedCommand
{
    public Command Command { get; }
    public string Action { get;  }
    public List<string> Arguments { get; }

    public ParsedCommand(Command command, string action, List<string> arguments)
    {
        Command = command;
        Action = action;
        Arguments = arguments;
    }
    
    public ParsedCommand(Command command, string action)
    {
        Command = command;
        Action = action;
        Arguments = [];
    }
}