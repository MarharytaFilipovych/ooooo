using RecipeManager.CommandDefinitions.ShoppingDefinitions;
using RecipeManager.Executors.ShoppingExecutors;
using RecipeManager.Storage;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.EventPublishing;
using RecipeManager.Entities;

namespace RecipeManager.CommandFactory;

public class ShoppingCommandSubFactory(
    IRecipeStorage recipeStorage, IStorage<Plan> planStorage, IEventPublisher eventPublisher) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new ShoppingExportDefinition(), new ShoppingExportExecutor(recipeStorage, planStorage, eventPublisher));
    }
}