using RecipeManager.Commands;

namespace RecipeManager.CommandDefinitions;

public interface ICommandDefinition
{
    string Name { get;  }
    string Description { get;  }
    bool TryParse(string[] args, out ICommand? command, out string? error);
}