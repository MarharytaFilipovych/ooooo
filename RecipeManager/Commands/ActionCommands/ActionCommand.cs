using RecipeManager.Commands.BaseCommands;

namespace RecipeManager.Commands.ActionCommands;

public class ActionCommand(string actionName, string recipeName, string? parameter = null)
    : CommandWithFirstArgName(recipeName) 
{
    public string ActionName { get; } = actionName;
    public string? Parameter { get; } = parameter; 
}
