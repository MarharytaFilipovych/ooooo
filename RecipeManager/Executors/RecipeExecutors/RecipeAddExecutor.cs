using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddExecutor(IStorage<Recipe> storage) :
    ICommandExecutor<CommandAdd>
{
    public ExecuteResult Execute(CommandAdd command)
    {
        var recipe = new Recipe(command.Name);
        
        Console.WriteLine(storage.Add(recipe)
            ? $"The recipe \"{command.Name}\" was successfully added!"
            : "The recipe with such a name already exists!");

        return ExecuteResult.Continue;
    }
}