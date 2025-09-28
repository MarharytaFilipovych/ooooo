namespace RecipeManager.Commands.PlanCommand;

public class PlanListCommand(string description) : ICommand
{
    public string Description { get; } = description;
    
    public void Execute()
    {
        throw new NotImplementedException();
    }
}