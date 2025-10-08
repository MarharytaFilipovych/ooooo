using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;
using RecipeManager.Events;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddExecutor(
    ISessionStorage sessionStorage, 
    IRecipeStorage recipeStorage, 
    IPlanValidator planValidator,
    IEventPublisher eventPublisher) : ICommandExecutor<CommandAdd>
{
    public ExecuteResult Execute(CommandAdd command)
    {
        if (!sessionStorage.TryGetCurrentUser(out var currentUser))
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var validationResult = planValidator.CanAddRecipe(currentUser!.Subscription);
        if (!validationResult.IsValid)
        {
            Console.WriteLine(validationResult.ErrorMessage);
            eventPublisher.Publish(new LimitReachedEvent("recipes"));
            return ExecuteResult.Continue;
        }
        
        var recipe = new Recipe(command.Name);
        
        var added = recipeStorage.Add(recipe);
        if (added)
        {
            eventPublisher.Publish(new RecipeCreatedEvent(command.Name));
            Console.WriteLine($"The recipe \"{command.Name}\" was successfully added!");
        }
        else
        {
            Console.WriteLine("The recipe with such a name already exists!");
        }

        return ExecuteResult.Continue;
    }
}