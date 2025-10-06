using RecipeManager.CommandDefinitions.ShoppingDefinitions;
using RecipeManager.Entities;
using RecipeManager.Executors.ShoppingExecutors;
using RecipeManager.Storage;
using RecipeManager.Storage.RecipeStorage;

namespace RecipeManager.CommandFactory;

public class ShoppingCommandSubFactory(IRecipeStorage recipeStorage, IStorage<Plan> planStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ShoppingExportDefinition(),
            new ShoppingExportExecutor(recipeStorage, planStorage));
    }
}