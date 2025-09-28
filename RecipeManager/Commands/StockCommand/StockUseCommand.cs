namespace RecipeManager.Commands.StockCommand;

public class StockUseCommand(string description) : ICommand
{
    public string Description { get; } = description;

    public void Execute()
    {
        throw new NotImplementedException();
    }
}