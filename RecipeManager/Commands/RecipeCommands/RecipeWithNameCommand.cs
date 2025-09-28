namespace RecipeManager.Commands.RecipeCommands;

public abstract class RecipeWithNameCommand(string name) : ICommand
{
    public string Name { get; } = name;

}