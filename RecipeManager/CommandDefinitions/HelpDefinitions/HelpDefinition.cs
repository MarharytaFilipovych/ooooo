using RecipeManager.Commands.HelpCommands;

namespace RecipeManager.CommandDefinitions.HelpDefinitions;

public class HelpDefinition : ICommandDefinition
{
    public string Name => "help";
    public string Description => "help";
    public ParseResult Parse(string[] args) => new(new HelpCommand());
    
}