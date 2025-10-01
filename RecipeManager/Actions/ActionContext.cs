using RecipeManager.Entities;
using RecipeManager.Storage;

namespace RecipeManager.Actions;

public class ActionContext
{
    public Recipe Recipe { get; }
    public string? Parameter { get; }
    public IIngredientStorage IngredientStorage { get; }
    public IRecipeStorage RecipeStorage { get; }

    public ActionContext(Recipe recipe, IIngredientStorage ingredientStorage, IRecipeStorage recipeStorage,
        string? parameter = null)
    {
        Recipe = recipe;
        IngredientStorage = ingredientStorage;
        RecipeStorage = recipeStorage;
        Parameter = parameter;
    }
}
