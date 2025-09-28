using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.StockExecutors;

public class StockInfoExecutor(IStorage<Ingredient> storage) :
    ICommandExecutor<StockInfoCommand>
{
    public ExecuteResult Execute(StockInfoCommand command)
    {
        storage.GetAll().ForEach(Console.WriteLine);
        return ExecuteResult.Continue;
    }
}