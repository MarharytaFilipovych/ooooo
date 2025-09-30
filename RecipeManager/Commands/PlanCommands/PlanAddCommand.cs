using RecipeManager.Commands.BaseCommands;

namespace RecipeManager.Commands.PlanCommands;

public class PlanAddCommand (string name, string recipeName, DateTime date, decimal? servingsMultiplier) : CommandWithFirstArgName(recipeName)
{
    public string RecipeName { get; } = recipeName;
    public DateTime Date { get; } = date;
    public decimal? ServingsMultiplier { get; } = servingsMultiplier;
}