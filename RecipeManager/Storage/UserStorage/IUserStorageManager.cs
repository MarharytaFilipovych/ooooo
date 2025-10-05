using RecipeManager.Entities;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;

namespace RecipeManager.Storage.UserStorage;

public interface IUserStorageManager
{
    IIngredientStorage GetIngredientStorage(string username);
    IRecipeStorage GetRecipeStorage(string username);
    IStorage<Plan> GetPlanStorage(string username);
}