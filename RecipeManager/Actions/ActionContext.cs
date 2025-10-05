using RecipeManager.Entities;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;

namespace RecipeManager.Actions;

public class ActionContext(
    Recipe recipe,
    IIngredientStorage ingredientStorage,
    IRecipeStorage recipeStorage,
    string? parameter = null)
{
    public Recipe Recipe { get; } = recipe;
    public string? Parameter { get; } = parameter;
    public IIngredientStorage IngredientStorage { get; } = ingredientStorage;
    public IRecipeStorage RecipeStorage { get; } = recipeStorage;
}
