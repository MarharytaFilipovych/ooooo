using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryIngredientStorage : IIngredientStorage
{
    private static readonly Dictionary<string, Ingredient> Ingredients = new(StringComparer.OrdinalIgnoreCase);
    
    public bool Add(Ingredient entity) => Ingredients.TryAdd(entity.Name, entity);

    public bool Remove(string name) => Ingredients.Remove(name);

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

    public int GetTotalQuantity() => Ingredients.Count;

    public List<Ingredient> GetAll() => Ingredients.Values.ToList();

    public bool Consume(Ingredient ingredient)
    {
        if (!Ingredients.TryGetValue(ingredient.Name, out var value)) return false;
        if (value.Quantity < ingredient.Quantity) return false;
        value.Quantity -= ingredient.Quantity;
        Ingredients[ingredient.Name] = value;
        return true;
    }
}