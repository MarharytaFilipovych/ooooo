using RecipeManager.Commands.BaseCommands;

namespace RecipeManager.Commands.RecipeCommands;

public class CommandAdd(string name) : CommandWithFirstArgName(name) { }