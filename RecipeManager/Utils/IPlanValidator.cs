using RecipeManager.Entities;

namespace RecipeManager.Utils;

public interface IPlanValidator
{
    ValidationResult CanAddPantryItem(string username);
    ValidationResult CanAddRecipe(string username);
    ValidationResult CanAddPlan(string username);
    ValidationResult CanChangePlan(string username, PlanType newPlanType);
}