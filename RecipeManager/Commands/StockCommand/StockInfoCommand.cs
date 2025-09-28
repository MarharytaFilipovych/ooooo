namespace RecipeManager.Commands.StockCommand;

public class StockInfoCommand(string description) : ICommand
{
    public string Description { get; } = description;

    public void Execute()
    {
        throw new NotImplementedException();
    }
}