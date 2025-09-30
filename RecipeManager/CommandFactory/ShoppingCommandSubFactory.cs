using RecipeManager.CommandDefinitions.ShoppingDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.ShoppingExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class ShoppingCommandSubFactory(IStorage<Plan> planStorage, IRecipeStorage recipeStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ShoppingExportDefinition(),
            new ShoppingExportExecutor(planStorage, recipeStorage));
    }
}