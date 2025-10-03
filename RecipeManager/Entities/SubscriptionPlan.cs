namespace RecipeManager.Entities;

public class SubscriptionPlan
{
    public PlanType Type { get; }
    public int MaxPantryItems { get; }
    public int MaxRecipes { get; }
    public int MaxPossibleEntries { get; }

    public SubscriptionPlan(PlanType type, int maxPantryItems, int maxRecipes, int maxPossibleEntries)
    {
        Type = type;
        MaxPantryItems = maxPantryItems;
        MaxRecipes = maxRecipes;
        MaxPossibleEntries = maxPossibleEntries;
    }
    
    public override string ToString()
    {
        return $"{Type} plan: \nPantry - {MaxPantryItems}, \nRecipes - {MaxRecipes}, \nPossible entries - {MaxPossibleEntries}";
    }
}