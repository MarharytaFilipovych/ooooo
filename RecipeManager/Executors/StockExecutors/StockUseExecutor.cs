using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.EventPublishing;
using RecipeManager.Events;

namespace RecipeManager.Executors.StockExecutors;

public class StockUseExecutor(
    IIngredientStorage ingredientStorage,
    IEventPublisher eventPublisher) : ICommandExecutor<StockUseCommand>
{
    public ExecuteResult Execute(StockUseCommand command)
    {
        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        
        if (!ingredientStorage.Consume(ingredient))
        { 
            Console.WriteLine("Could not consume an ingredient because of its insufficient quantity in the stock!");
            return ExecuteResult.Continue;
        }
        
        eventPublisher.Publish(new StockUsedEvent(command.IngredientName, command.Quantity, command.Unit, command.Reason));
        
        Console.WriteLine($"The ingredient \"{ingredient}\" was successfully consumed");
        if (!string.IsNullOrWhiteSpace(command.Reason))
        {
            Console.WriteLine($"The reason of consumption is: {command.Reason}");
        }

        return ExecuteResult.Continue;
    }
}