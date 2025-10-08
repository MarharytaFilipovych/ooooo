using RecipeManager.Commands;
using RecipeManager.Commands.StockCommands;
using RecipeManager.Entities;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;
using RecipeManager.Events;

namespace RecipeManager.Executors.StockExecutors;

public class StockAddExecutor(
    ISessionStorage sessionStorage, 
    IIngredientStorage ingredientStorage,
    IPlanValidator planValidator,
    IEventPublisher eventPublisher) : ICommandExecutor<StockAddCommand>
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
            eventPublisher.Publish(new LimitReachedEvent("pantry_items"));
            return ExecuteResult.Continue;
        }

        var ingredient = new Ingredient(command.IngredientName, command.Quantity, command.Unit);
        
        var added = ingredientStorage.Add(ingredient);
        if (added)
        {
            eventPublisher.Publish(new StockAddedEvent(command.IngredientName, command.Quantity, command.Unit));
            
            Console.WriteLine($"Your ingredient \"{ingredient}\" was successfully added to our stock!");
        }
        else
        {
            Console.WriteLine($"Ingredient with the name \"{ingredient.Name}\" has already been added!");
        }
        
        return ExecuteResult.Continue;
    }
}