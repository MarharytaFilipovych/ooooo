using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryIngredientStorage : IStorage<Ingredient>
{
    private static readonly Dictionary<string, Ingredient> Ingredients = new();
    
    public bool Add(Ingredient entity)
    {
        return Ingredients.TryAdd(entity.Name, entity);
    }

    public bool Remove(string name)
    {
        return Ingredients.Remove(name);
    }

    public bool Update(Ingredient entity)
    {
        if (!Ingredients.ContainsKey(entity.Name)) return false;
        Ingredients[entity.Name] = entity;
        return true;
    }

    public Ingredient? GetEntityByName(string name)
    {
        Ingredients.TryGetValue(name, out var ingredient);
        return ingredient;
    }

    public int GetTotalQuantity()
    {
        return Ingredients.Count;
    }

    public List<Ingredient> GetAll()
    {
        return Ingredients.Values.ToList();
    }

    public Ingredient? Consume(Ingredient ingredient)
    {
        if (!Ingredients.TryGetValue(ingredient.Name, out var value)) return null;
        Ingredients.Remove(value.Name);
        return value;

    }
}