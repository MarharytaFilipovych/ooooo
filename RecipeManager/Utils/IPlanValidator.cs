using RecipeManager.Entities;

namespace RecipeManager.Utils;

public interface IPlanValidator
{
    ValidationResult CanAddPantryItem(SubscriptionPlan subscriptionPlan);
    ValidationResult CanAddRecipe(SubscriptionPlan subscriptionPlan);
    ValidationResult CanAddPlan(SubscriptionPlan subscriptionPlan);
    ValidationResult CanChangePlan(PlanType newPlanType);
}