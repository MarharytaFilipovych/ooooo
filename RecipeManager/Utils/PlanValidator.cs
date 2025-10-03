using RecipeManager.Factories;
using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Utils;

public class PlanValidator(
    IUserStorage userStorage,
    ISubscriptionStorage subscriptionStorage,
    UserStorageManager storageManager) : IPlanValidator
{
    public ValidationResult CanAddPantryItem(string username)
    {
        var subscription = subscriptionStorage.GetSubscription(username);
        if (subscription == null)
            return ValidationResult.Failure("User subscription not found");

        var ingredientStorage = storageManager.GetIngredientStorage(username);
        var currentCount = ingredientStorage.GetTotalQuantity();

        if (currentCount >= subscription.Plan.MaxPantryItems)
        {
            return ValidationResult.Failure(
                $"Cannot add pantry item. Your {subscription.Plan.Type} plan allows maximum " +
                $"{subscription.Plan.MaxPantryItems} items. Current: {currentCount}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult CanAddRecipe(string username)
    {
        var subscription = subscriptionStorage.GetSubscription(username);
        if (subscription == null)
            return ValidationResult.Failure("User subscription not found");

        var recipeStorage = storageManager.GetRecipeStorage(username);
        var currentCount = recipeStorage.GetTotalQuantity();

        if (currentCount >= subscription.Plan.MaxRecipes)
        {
            return ValidationResult.Failure(
                $"Cannot add recipe. Your {subscription.Plan.Type} plan allows maximum " +
                $"{subscription.Plan.MaxRecipes} recipes. Current: {currentCount}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult CanAddPlan(string username)
    {
        var subscription = subscriptionStorage.GetSubscription(username);
        if (subscription == null)
            return ValidationResult.Failure("User subscription not found");

        var planStorage = storageManager.GetPlanStorage(username);
        var currentCount = planStorage.GetTotalQuantity();

        if (currentCount >= subscription.Plan.MaxPossibleEntries)
        {
            return ValidationResult.Failure(
                $"Cannot add meal plan. Your {subscription.Plan.Type} plan allows maximum " +
                $"{subscription.Plan.MaxPossibleEntries} planned entries. Current: {currentCount}");
        }

        return ValidationResult.Success();
    }

    public ValidationResult CanChangePlan(string username, PlanType newPlanType)
    {
        var newPlan = SubscriptionPlanFactory.CreatePlan(newPlanType);
        
        var ingredientStorage = storageManager.GetIngredientStorage(username);
        var recipeStorage = storageManager.GetRecipeStorage(username);
        var planStorage = storageManager.GetPlanStorage(username);

        var pantryCount = ingredientStorage.GetTotalQuantity();
        var recipeCount = recipeStorage.GetTotalQuantity();
        var planCount = planStorage.GetTotalQuantity();

        var errors = new List<string>();

        if (pantryCount > newPlan.MaxPantryItems)
        {
            errors.Add($"Pantry items: {pantryCount} exceeds {newPlan.Type} plan limit of {newPlan.MaxPantryItems}");
        }

        if (recipeCount > newPlan.MaxRecipes)
        {
            errors.Add($"Recipes: {recipeCount} exceeds {newPlan.Type} plan limit of {newPlan.MaxRecipes}");
        }

        if (planCount > newPlan.MaxPossibleEntries)
        {
            errors.Add($"Meal plans: {planCount} exceeds {newPlan.Type} plan limit of {newPlan.MaxPossibleEntries}");
        }

        if (errors.Any())
        {
            var errorMessage = $"Cannot downgrade to {newPlan.Type} plan:\n" + string.Join("\n", errors);
            return ValidationResult.Failure(errorMessage);
        }

        return ValidationResult.Success();
    }
}