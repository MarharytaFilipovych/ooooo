using RecipeManager.Entities;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;

namespace RecipeManager.Storage.UserStorage;

public class MultiUserStorageManager(
    Func<IIngredientStorage> ingredientStorageFactory,
    Func<IRecipeStorage> recipeStorageFactory,
    Func<IStorage<Plan>> planStorageFactory)
    : IUserStorageManager
{
    private readonly Dictionary<string, IIngredientStorage> _userIngredientStorages =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IRecipeStorage> _userRecipeStorages =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IStorage<Plan>> _userPlanStorages =
        new(StringComparer.OrdinalIgnoreCase);
    

    public IIngredientStorage GetIngredientStorage(string username)
    {
        if (_userIngredientStorages.TryGetValue(username, out var storage)) return storage;
        storage = ingredientStorageFactory();
        _userIngredientStorages[username] = storage;
        return storage;
    }
    
    public IRecipeStorage GetRecipeStorage(string username)
    {
        if (_userRecipeStorages.TryGetValue(username, out var storage)) return storage;
        storage = recipeStorageFactory();
        _userRecipeStorages[username] = storage;
        return storage;
    }
    
    public IStorage<Plan> GetPlanStorage(string username)
    {
        if (_userPlanStorages.TryGetValue(username, out var storage)) return storage;
        storage = planStorageFactory();
        _userPlanStorages[username] = storage;
        return storage;
    }
}