using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.PlanStorage;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;

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
        
        var eventPublisher = new EventPublisher();
        var eventWriter = new EventWriter("../../../events.log");
        eventPublisher.Subscribe(eventWriter);
        
        var context = new Context();
        context.SetUserStorage(sessionStorage);
        
        var subfactories = new ICommandSubFactory[]
        {
            new LoginCommandSubFactory(sessionStorage, eventPublisher),
            new StockCommandSubFactory(sessionStorage, ingredientStorage, planValidator, eventPublisher),
            new RecipeCommandSubFactory(sessionStorage, recipeStorage, planValidator, eventPublisher),
            new PlanCommandSubFactory(sessionStorage, planStorage, planValidator, eventPublisher),
            new ShoppingCommandSubFactory(recipeStorage, planStorage, eventPublisher),
            new ActionCommandSubFactory(recipeStorage, ingredientStorage, eventPublisher),
            new ExitCommandSubFactory(sessionStorage),
            new ChangePlanCommandSubFactory(sessionStorage, planValidator),
            new HelpCommandSubFactory()
        };
        
        foreach (var subfactory in subfactories)
        {
            subfactory.Create(context);
        }
        
        return context;
    }
}