using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public static class CommandFactory
{
    public static Context Create()
    {
        var ingredientStorage = new InMemoryIngredientStorage();
        var recipeStorage = new InMemoryRecipeStorage();
        var planStorage = new InMemoryPlanStorage();
        
        var context = new Context();
        
        var subfactories = new ICommandSubFactory[]
        {
            new StockCommandSubFactory(ingredientStorage),
            new RecipeCommandSubFactory(recipeStorage),
            new PlanCommandSubFactory(planStorage),
            new ShoppingCommandSubFactory(planStorage, recipeStorage),
            new ActionCommandSubFactory(recipeStorage, ingredientStorage),
            new HelpCommandSubFactory(),
            new ExitCommandSubFactory()
        };
        
        foreach (var subfactory in subfactories)
        {
            subfactory.Create(context);
        }
        
        return context;
    }
}