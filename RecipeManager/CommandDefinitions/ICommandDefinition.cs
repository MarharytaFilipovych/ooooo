using RecipeManager.Commands;

namespace RecipeManager.CommandDefinitions;

public interface ICommandDefinition
{
    string Name { get;  }
    string Description { get;  }
    ParseResult Parse(string[] args);
}