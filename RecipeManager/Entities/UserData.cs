using RecipeManager.Factories;

namespace RecipeManager.Entities;

public class UserData
{
    public string Username { get; set; } = string.Empty;
    public required SubscriptionPlan Subscription { get; set; } = SubscriptionPlanFactory.CreateBasicPlan();
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<Recipe> Recipes { get; set; } = new();
    public List<Plan> Plans { get; set; } = new();
}