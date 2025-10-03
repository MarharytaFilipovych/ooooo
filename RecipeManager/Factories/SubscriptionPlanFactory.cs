using RecipeManager.Entities;

namespace RecipeManager.Factories;

public class SubscriptionPlanFactory
{
    public static SubscriptionPlan CreateBasicPlan()
    {
        return new SubscriptionPlan(
            type: PlanType.Basic,
            maxPantryItems: 15,
            maxRecipes: 10,
            maxPossibleEntries: 30
        );
    }
    
    public static SubscriptionPlan CreateChefPlan()
    {
        return new SubscriptionPlan(
            type: PlanType.Chef,
            maxPantryItems: 200,
            maxRecipes: 150,
            maxPossibleEntries: 500
        );
    }
    
    public static SubscriptionPlan CreatePlan(PlanType type)
    {
        return type switch
        {
            PlanType.Basic => CreateBasicPlan(),
            PlanType.Chef => CreateChefPlan(),
            _ => throw new ArgumentException($"We don't know such a plan type: {type}. " +
                                             "There are only Basic and Chef plans just yet.")
        };
    }
}