using RecipeManager;
using RecipeManager.CommandFactory;

var context = CommandFactory.Create();
var loop = new CommandsLoop(context);
loop.Run();