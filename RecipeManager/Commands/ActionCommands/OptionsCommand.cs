using RecipeManager.Commands.BaseCommands;

namespace RecipeManager.Commands.ActionCommands;

public class OptionsCommand(string recipeName) : CommandWithFirstArgName(recipeName)
{
}