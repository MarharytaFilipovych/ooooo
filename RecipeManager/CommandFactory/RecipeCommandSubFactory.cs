using RecipeManager.CommandDefinitions.RecipeDefinitions;
using RecipeManager.Executors.RecipeExecutors;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.Storage.SessionStorage;
using RecipeManager.Utils;
using RecipeManager.EventPublishing;

namespace RecipeManager.CommandFactory;

public class RecipeCommandSubFactory(
    ISessionStorage sessionStorage, 
    IRecipeStorage recipeStorage, 
    IPlanValidator planValidator,
    IEventPublisher eventPublisher) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new RecipeAddDefinition(), new RecipeAddExecutor(sessionStorage, recipeStorage, planValidator, eventPublisher));
        context.Register(new RecipeAddIngredientDefinition(), new RecipeAddIngredientExecutor(recipeStorage, eventPublisher));
        context.Register(new RecipeAddStepsDefinition(), new RecipeAddStepsExecutor(recipeStorage));
        context.Register(new RecipeInfoDefinition(), new RecipeInfoExecutor(recipeStorage));
        context.Register(new RecipeListDefinition(), new RecipeListExecutor(recipeStorage));
    }
}