using RecipeManager.Commands;

namespace RecipeManager.CommandDefinitions;

public class ParseResult
{
    public ICommand? Command { get; set; } = null;
    public string? Error { get; set; } = null;

    public ParseResult(ICommand command)
    {
        Command = command;
    }

    public ParseResult() { }
}