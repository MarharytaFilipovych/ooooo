using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeListExecutor(IStorage<Recipe> storage) : ICommandExecutor<RecipeListCommand>
{
    public ExecuteResult Execute(RecipeListCommand command)
    {
        var recipes = storage.GetAll();
        
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes found. Add one with: recipe add \"<recipe_name>\"");
            return ExecuteResult.Continue;
        }
        
        Console.WriteLine($"Found {recipes.Count} recipe(s):\n");
        
        for (var i = 0; i < recipes.Count; i++)
        {
            var recipe = recipes[i];
            Console.WriteLine($"{i + 1}. {recipe}");
        }
        
        return ExecuteResult.Continue;
    }
}