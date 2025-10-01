using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.StockExecutors;

public class StockUseExecutor(IUserStorage userStorage, UserStorageManager storageManager) :
    ICommandExecutor<StockUseCommand>
{
    public ExecuteResult Execute(StockUseCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var storage = storageManager.GetIngredientStorage(currentUser.Username);
        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        if (!storage.Consume(ingredient))
        { 
            Console.WriteLine("Could not consume an ingredient because of its insufficient quantity in the stock!");
            return ExecuteResult.Continue;
        }
        
        Console.WriteLine($"The ingredient \"{ingredient}\" was successfully consumed");
        if (!string.IsNullOrWhiteSpace(command.Reason))
        {
            Console.WriteLine($"The reason of consumption is: {command.Reason}");
        }

        return ExecuteResult.Continue;

    }
}