using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeListExecutor(IStorage<Recipe> storage) : ICommandExecutor<RecipeListCommand>
{
    public ExecuteResult Execute(RecipeListCommand command)
    {
        storage.GetAll().ForEach(Console.WriteLine);
        return ExecuteResult.Continue;
    }
}