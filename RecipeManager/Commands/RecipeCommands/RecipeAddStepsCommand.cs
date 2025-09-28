using RecipeManager.Commands.BaseCommands;

namespace RecipeManager.Commands.RecipeCommands;

public class CommandAddSteps(string name, string stepText) : CommandWithFirstArgName(name)
{
    public string StepText { get; } = stepText;

}