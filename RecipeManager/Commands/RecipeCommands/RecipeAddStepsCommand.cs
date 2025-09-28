namespace RecipeManager.Commands.RecipeCommands;

public class RecipeAddStepsCommand(string name, string stepText) : RecipeWithNameCommand(name)
{
    public string StepText { get; } = stepText;

}