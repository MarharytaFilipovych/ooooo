using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddExecutor(ISessionStorage sessionStorage, IStorage<Recipe> recipeStorage, 
    IPlanValidator planValidator) : ICommandExecutor<CommandAdd>
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
            return ExecuteResult.Continue;
        }
        
        var recipe = new Recipe(command.Name);
        
        Console.WriteLine(recipeStorage.Add(recipe)
            ? $"The recipe \"{command.Name}\" was successfully added!"
            : "The recipe with such a name already exists!");

        return ExecuteResult.Continue;
    }
}