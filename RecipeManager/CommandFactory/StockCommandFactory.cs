using RecipeManager.CommandDefinitions.StockDefinitions;
using RecipeManager.Executors.StockExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class StockCommandSubFactory(IIngredientStorage ingredientStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new StockAddDefinition(), new StockAddExecutor(ingredientStorage));
        context.Register(new StockInfoDefinition(), new StockInfoExecutor(ingredientStorage));
        context.Register(new StockUseDefinition(), new StockUseExecutor(ingredientStorage));
    }
}