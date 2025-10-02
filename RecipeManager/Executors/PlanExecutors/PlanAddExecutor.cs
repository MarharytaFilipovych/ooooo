using RecipeManager.Commands;
using RecipeManager.Commands.PlanCommands;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Executors.PlanExecutors;

public class PlanAddExecutor(IUserStorage userStorage, UserStorageManager storageManager) : ICommandExecutor<PlanAddCommand>
{
    public ExecuteResult Execute(PlanAddCommand command)
    {
        var currentUser = userStorage.GetCurrentUser();
        if (currentUser == null)
        {
            Console.WriteLine("You must login first!");
            return ExecuteResult.Continue;
        }

        var planStorage = storageManager.GetPlanStorage(currentUser.Username);
        var plan = new Plan(command.Name, command.RecipeName, command.Date, command.ServingsMultiplier);
        Console.WriteLine(
            planStorage.Add(plan)
                ? $"Plan \"{command.Name}\" was successfully added!"
                : $"Plan with a name \"{command.Name}\" has already been added!"
        );
        return ExecuteResult.Continue;
    }
}