using RecipeManager.Entities;

namespace RecipeManager.Storage;

public interface IUserStorageManager
{
    IIngredientStorage GetIngredientStorage(string username);
    IRecipeStorage GetRecipeStorage(string username);
    IStorage<Plan> GetPlanStorage(string username);
}