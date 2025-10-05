using RecipeManager.Entities;

namespace RecipeManager.Storage.RecipeStorage;

public class InMemoryRecipeStorage: IRecipeStorage
{
    private readonly Dictionary<string, Recipe> _recipes = new(StringComparer.OrdinalIgnoreCase);

    public bool Add(Recipe entity) => _recipes.TryAdd(entity.Name, entity);

    public bool Remove(string name) => _recipes.Remove(name);

    public bool Update(Recipe entity)
    {
        if (!_recipes.ContainsKey(entity.Name)) return false;
        _recipes[entity.Name] = entity;
        return true;
    }

    public Recipe? GetEntityByName(string name)
    {
        _recipes.TryGetValue(name, out var recipe);
        return recipe;
    }

    public int GetTotalQuantity() => _recipes.Count;
    
    public List<Recipe> GetAll() => _recipes.Values.ToList();
    public bool ExistsByName(string name) => _recipes.ContainsKey(name);
    
    public void AddAll(IEnumerable<Recipe> recipes)
    {
        foreach (var recipe in recipes)
        {
            _recipes.TryAdd(recipe.Name, recipe);
        }
    }

    public void RemoveAll()
    {
        _recipes.Clear();
    }

    public List<Recipe> GetRecipesByNames(IEnumerable<string> names)
    {
        return names
            .Where(name => _recipes.ContainsKey(name))
            .Select(name => _recipes[name])
            .ToList();
    }
}