using RecipeManager.Commands.BaseCommands;

namespace RecipeManager.Commands.RecipeCommands;

public class CommandInfo(string name) : CommandWithFirstArgName(name) { }