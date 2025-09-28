using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddExecutor(IStorage<Recipe> storage) :
    ICommandExecutor<RecipeAddCommand>
{
    public ExecuteResult Execute(RecipeAddCommand command)
    {
        var recipe = new Recipe(command.Name);
        
        Console.WriteLine(storage.Add(recipe)
            ? $"The recipe {recipe} was successfully added!"
            : "The recipe with such a name already exists!");

        return ExecuteResult.Continue;
    }
}