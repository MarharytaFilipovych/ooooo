using RecipeManager.Factories;
using RecipeManager.Entities;
using RecipeManager.Storage;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;

namespace RecipeManager.Utils;

public class PlanValidator(IIngredientStorage  ingredientStorage, IStorage<Plan> planStorage, 
    IRecipeStorage recipeStorage) : IPlanValidator
{
    public ValidationResult CanAddPantryItem(SubscriptionPlan subscriptionPlan)
    {

        var currentCount = ingredientStorage.GetTotalQuantity();

        if (currentCount >= subscriptionPlan.MaxPantryItems)
        {
            return ValidationResult.Failure(
                $"Cannot add pantry item. Your {subscriptionPlan.Type} plan allows maximum " +
                $"{subscriptionPlan.MaxPantryItems} items. Current: {currentCount}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult CanAddRecipe(SubscriptionPlan subscriptionPlan)
    {
        var currentCount = recipeStorage.GetTotalQuantity();

        if (currentCount >= subscriptionPlan.MaxRecipes)
        {
            return ValidationResult.Failure(
                $"Cannot add recipe. Your {subscriptionPlan.Type} plan allows maximum " +
                $"{subscriptionPlan.MaxRecipes} recipes. Current: {currentCount}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult CanAddPlan(SubscriptionPlan subscriptionPlan)
    {
        var currentCount = planStorage.GetTotalQuantity();

        if (currentCount >= subscriptionPlan.MaxPossibleEntries)
        {
            return ValidationResult.Failure(
                $"Cannot add meal plan. Your {subscriptionPlan.Type} plan allows maximum " +
                $"{subscriptionPlan.MaxPossibleEntries} planned entries. Current: {currentCount}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult CanChangePlan(PlanType newPlanType)
    {
        var newPlan = SubscriptionPlanFactory.CreatePlan(newPlanType);

        var pantryCount = ingredientStorage.GetTotalQuantity();
        var recipeCount = recipeStorage.GetTotalQuantity();
        var planCount = planStorage.GetTotalQuantity();

        var errors = new List<string>();

        if (pantryCount > newPlan.MaxPantryItems)
        {
            errors.Add($"Pantry items: {pantryCount} exceeds " +
                       $"{newPlan.Type} plan limit of {newPlan.MaxPantryItems}");
        }

        if (recipeCount > newPlan.MaxRecipes)
        {
            errors.Add($"Recipes: {recipeCount} exceeds " +
                       $"{newPlan.Type} plan limit of {newPlan.MaxRecipes}");
        }

        if (planCount > newPlan.MaxPossibleEntries)
        {
            errors.Add($"Meal plans: {planCount} exceeds" +
                       $" {newPlan.Type} plan limit of {newPlan.MaxPossibleEntries}");
        }

        if (errors.Count == 0) return ValidationResult.Success();
        var errorMessage = $"Cannot downgrade to {newPlan.Type} plan:\n" + string.Join("\n", errors);
        return ValidationResult.Failure(errorMessage);
    }
}