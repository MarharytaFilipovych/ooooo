using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.StockExecutors;

public class StockInfoExecutor(IUserStorage userStorage, UserStorageManager storageManager) :
    ICommandExecutor<StockInfoCommand>
{
    public ExecuteResult Execute(StockInfoCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var storage = storageManager.GetIngredientStorage(currentUser.Username);
        storage.GetAll().ForEach(Console.WriteLine);
        return ExecuteResult.Continue;
    }
}