using RecipeManager.CommandDefinitions.RecipeDefinitions;
using RecipeManager.Executors.RecipeExecutors;
using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public class RecipeCommandSubFactory(IRecipeStorage recipeStorage, 
    IIngredientStorage ingredientStorage) : ICommandSubFactory
{
    public void Create(Context context)
    {
        context.Register(new RecipeAddDefinition(), new RecipeAddExecutor(recipeStorage));
        context.Register(new RecipeAddIngredientDefinition(), 
            new RecipeAddIngredientExecutor(recipeStorage, ingredientStorage));
        context.Register(new RecipeAddStepsDefinition(), new RecipeAddStepsExecutor(recipeStorage));
        context.Register(new RecipeInfoDefinition(), new RecipeInfoExecutor(recipeStorage));
        context.Register(new RecipeListDefinition(), new RecipeListExecutor(recipeStorage));
    }
}