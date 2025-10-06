using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.StockExecutors;

public class StockInfoExecutor(IStorage<Ingredient> ingredientStorage) :
    ICommandExecutor<StockInfoCommand>
{
    public ExecuteResult Execute(StockInfoCommand command)
    {
        ingredientStorage.GetAll().ForEach(Console.WriteLine);
        return ExecuteResult.Continue;
    }
}