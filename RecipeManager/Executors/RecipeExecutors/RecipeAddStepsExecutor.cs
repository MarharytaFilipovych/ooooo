using RecipeManager.Commands;
using RecipeManager.Commands.RecipeCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.RecipeExecutors;

public class RecipeAddStepsExecutor(IStorage<Recipe> recipeStorage) : 
    ICommandExecutor<CommandAddSteps>
{
    public ExecuteResult Execute(CommandAddSteps command)
    {
        var recipe = recipeStorage.GetEntityByName(command.Name);
        if (recipe == null)
        {
            Console.WriteLine($"Recipe with a name \"{command.Name}\" " +
                              $"was not found! No steps added.");
            return ExecuteResult.Continue;
        }
        recipe.AddSteps(command.StepText);
        Console.WriteLine($"Step \"{command.StepText}\" was successfully added to this great recipe!");
        return ExecuteResult.Continue;
    }
}