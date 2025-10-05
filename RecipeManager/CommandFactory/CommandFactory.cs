using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.PlanStorage;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;

namespace RecipeManager.CommandFactory;

public static class CommandFactory
{
    public static Context Create()
    {
        var ingredientStorage = new InMemoryIngredientStorage();
        var planStorage = new InMemoryPlanStorage();
        var recipeStorage = new InMemoryRecipeStorage();
        var sessionStorage = new JsonSessionStorage(ingredientStorage, recipeStorage, planStorage);

        
        var planValidator = new PlanValidator(ingredientStorage, planStorage, recipeStorage);
        
        var context = new Context();
        context.SetUserStorage(sessionStorage);
        
        var subfactories = new ICommandSubFactory[]
        {
            new LoginCommandSubFactory(sessionStorage),
            new StockCommandSubFactory(sessionStorage, ingredientStorage, planValidator),
            new RecipeCommandSubFactory(sessionStorage, recipeStorage, planValidator),
            new PlanCommandSubFactory(sessionStorage, planStorage, planValidator),
            new ShoppingCommandSubFactory(recipeStorage, planStorage),
            new ActionCommandSubFactory(recipeStorage, ingredientStorage),
            new HelpCommandSubFactory(),
            new ExitCommandSubFactory(sessionStorage),
            new ChangePlanCommandSubFactory(sessionStorage, planValidator)
        };
        
        foreach (var subfactory in subfactories)
        {
            subfactory.Create(context);
        }
        
        return context;
    }
}