using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryRecipeStorage: IStorage<Recipe>
{
    private static readonly Dictionary<string, Recipe> Recipes = new(StringComparer.OrdinalIgnoreCase);

    public bool Add(Recipe entity) => Recipes.TryAdd(entity.Name, entity);

    public bool Remove(string name) => Recipes.Remove(name);

    public bool Update(Recipe entity)
    {
        if (!Recipes.ContainsKey(entity.Name)) return false;
        Recipes[entity.Name] = entity;
        return true;
    }

    public Recipe? GetEntityByName(string name)
    {
        Recipes.TryGetValue(name, out var recipe);
        return recipe;
    }

    public int GetTotalQuantity() => Recipes.Count;
    
    public List<Recipe> GetAll() => Recipes.Values.ToList();
    public bool ExistsByName(string name) => Recipes.ContainsKey(name);
}