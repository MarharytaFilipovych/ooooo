namespace RecipeManager.Commands.BaseCommands;

public abstract class CommandWithFirstArgName(string name) : ICommand
{
    public string Name { get; } = name;

}