namespace RecipeManager.Commands.RecipeCommand;

public class RecipeInfoCommand(string description) : ICommand
{
    public string Description { get; } = description;

    public void Execute()
    {
        throw new NotImplementedException();
    }
}