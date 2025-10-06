using RecipeManager.Entities;

namespace RecipeManager.Commands.ChangePlanCommands;

public class ChangePlanCommand(PlanType newPlanType) : ICommand
{
    public PlanType NewPlanType { get; } = newPlanType;
}