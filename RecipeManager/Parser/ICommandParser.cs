namespace RecipeManager.Parser;

public interface ICommandParser
{
    ParsedCommand Parse(string raw);
}