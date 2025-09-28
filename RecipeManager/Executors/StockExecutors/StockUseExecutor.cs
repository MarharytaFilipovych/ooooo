using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.StockExecutors;

public class StockUseExecutor(IIngredientStorage storage) :
    ICommandExecutor<StockUseCommand>
{
    public ExecuteResult Execute(StockUseCommand command)
    {
        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        if (!storage.Consume(ingredient))
        { 
            Console.WriteLine("Could not consume an ingredient because of its insufficient quantity in stock!");
            return ExecuteResult.Continue;
        }
        
        Console.WriteLine($"The ingredient {ingredient} was successfully consumed");
        if (!string.IsNullOrWhiteSpace(command.Reason))
        {
            Console.WriteLine($"The reason of consumption is: {command.Reason}");
        }

        return ExecuteResult.Continue;

    }
}