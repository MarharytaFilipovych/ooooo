using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.Executors.StockExecutors;

public class StockAddExecutor(ISessionStorage sessionStorage, IStorage<Ingredient> ingredientStorage,
    IPlanValidator planValidator) : ICommandExecutor<StockAddCommand>
{
    public ExecuteResult Execute(StockAddCommand command)
    {
        if (!sessionStorage.TryGetCurrentUser(out var currentUser))
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }
        
        var validationResult = planValidator.CanAddPantryItem(currentUser!.Subscription);
        if (!validationResult.IsValid)
        {
            Console.WriteLine(validationResult.ErrorMessage);
            return ExecuteResult.Continue;
        }

        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        Console.WriteLine(ingredientStorage.Add(ingredient)
            ? $"Your ingredient \"{ingredient}\" was successfully added to our stock!"
            : $"Ingredient with the name \"{ingredient.Name}\" has already been added!");
        return ExecuteResult.Continue;
    }
}