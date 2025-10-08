using RecipeManager.CommandDefinitions.StockDefinitions;
using RecipeManager.Executors.StockExecutors;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;

namespace RecipeManager.CommandFactory;

public class StockCommandSubFactory(
    ISessionStorage sessionStorage, 
    IIngredientStorage ingredientStorage, 
    IPlanValidator planValidator,
    IEventPublisher eventPublisher) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new StockAddDefinition(), new StockAddExecutor(sessionStorage, ingredientStorage, planValidator, eventPublisher));
        context.Register(new StockInfoDefinition(), new StockInfoExecutor(ingredientStorage));
        context.Register(new StockUseDefinition(), new StockUseExecutor(ingredientStorage, eventPublisher));
    }
}