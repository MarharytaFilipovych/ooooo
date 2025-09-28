using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryRecipeStorage: IStorage<Recipe>
{
    private static readonly Dictionary<string, Recipe> Recipes = new();

    public bool Add(Recipe entity)
    {
        return Recipes.TryAdd(entity.Name, entity);
    }

    public bool Remove(string name)
    {
        return Recipes.Remove(name);
    }

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

    public int GetTotalQuantity()
    {
        return Recipes.Count;
    }

    public List<Recipe> GetAll()
    {
        return Recipes.Values.ToList();
    }
    
}