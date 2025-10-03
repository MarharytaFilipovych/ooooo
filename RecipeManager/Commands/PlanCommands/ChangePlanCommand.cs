using RecipeManager.Entities;

namespace RecipeManager.Commands.PlanCommands;

public class ChangePlanCommand : ICommand
{
    public PlanType NewPlanType { get; }

    public ChangePlanCommand(PlanType newPlanType)
    {
        NewPlanType = newPlanType;
    }
}