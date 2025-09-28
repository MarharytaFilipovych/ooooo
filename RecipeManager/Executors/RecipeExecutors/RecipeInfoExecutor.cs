using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeInfoExecutor(IStorage<Recipe> storage) : ICommandExecutor<RecipeInfoCommand>
{
    public ExecuteResult Execute(RecipeInfoCommand command)
    {
        var recipe = storage.GetEntityByName(command.Name);
        Console.WriteLine(recipe?.ToString() 
                          ?? $"Recipe with a name {command.Name} was not " +
                          $"found! No ingredients added.");
        
        return ExecuteResult.Continue;
      
    }
}