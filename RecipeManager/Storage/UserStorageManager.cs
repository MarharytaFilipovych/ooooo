using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class UserStorageManager
{
    private readonly Dictionary<string, IIngredientStorage> _userIngredientStorages =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IRecipeStorage> _userRecipeStorages =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IStorage<Plan>> _userPlanStorages =
        new(StringComparer.OrdinalIgnoreCase);

    public IIngredientStorage GetIngredientStorage(string username)
    {
        if (!_userIngredientStorages.TryGetValue(username, out var storage))
        {
            storage = new InMemoryIngredientStorage();
            _userIngredientStorages[username] = storage;
        }
        return storage;
    }
    
    public IRecipeStorage GetRecipeStorage(string username)
    {
        if (!_userRecipeStorages.TryGetValue(username, out var storage))
        {
            storage = new InMemoryRecipeStorage();
            _userRecipeStorages[username] = storage;
        }
        return storage;
    }
    
    public IStorage<Plan> GetPlanStorage(string username)
    {
        if (!_userPlanStorages.TryGetValue(username, out var storage))
        {
            storage = new InMemoryPlanStorage();
            _userPlanStorages[username] = storage;
        }
        return storage;
    }
}