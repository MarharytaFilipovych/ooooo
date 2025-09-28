using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.StockExecutors;

public class StockAddExecutor(IStorage<Ingredient> storage) :
    ICommandExecutor<StockAddCommand>
{
    public ExecuteResult Execute(StockAddCommand command)
    {
        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        Console.WriteLine(storage.Add(ingredient)
            ? $"Your ingredient {ingredient} was successfully added"
            : $"Ingredient with the name {ingredient.Name} has already been added!");
        return ExecuteResult.Continue;
    }
}