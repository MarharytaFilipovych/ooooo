namespace RecipeManager.Commands.RecipeCommand;

public class RecipeListCommand : ICommand
{
    public RecipeListCommand(string description)
    {
        Description = description;
    }

    public string Description { get; }
    
    public void Execute()
    {
        throw new NotImplementedException();
    }
}