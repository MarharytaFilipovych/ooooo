using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;
using RecipeManager.Utils;

namespace RecipeManager.Executors.StockExecutors;

public class StockAddExecutor(IUserStorage userStorage, UserStorageManager storageManager, IPlanValidator planValidator) :
    ICommandExecutor<StockAddCommand>
{
    public ExecuteResult Execute(StockAddCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }
        
        var validationResult = planValidator.CanAddPantryItem(currentUser.Username);
        if (!validationResult.IsValid)
        {
            Console.WriteLine(validationResult.ErrorMessage);
            return ExecuteResult.Continue;
        }

        var storage = storageManager.GetIngredientStorage(currentUser.Username);
        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        Console.WriteLine(storage.Add(ingredient)
            ? $"Your ingredient \"{ingredient}\" was successfully added to our stock!"
            : $"Ingredient with the name \"{ingredient.Name}\" has already been added!");
        return ExecuteResult.Continue;
    }
}