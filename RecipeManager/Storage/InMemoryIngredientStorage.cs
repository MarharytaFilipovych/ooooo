using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryIngredientStorage : IIngredientStorage
{
    private readonly Dictionary<string, Ingredient> _ingredients = new(StringComparer.OrdinalIgnoreCase);
    
    public bool Add(Ingredient entity) => _ingredients.TryAdd(entity.Name, entity);

    public bool Remove(string name) => _ingredients.Remove(name);

    public bool Update(Ingredient entity)
    {
        if (!_ingredients.ContainsKey(entity.Name)) return false;
        _ingredients[entity.Name] = entity;
        return true;
    }

    public Ingredient? GetEntityByName(string name)
    {
        _ingredients.TryGetValue(name, out var ingredient);
        return ingredient;
    }

    public int GetTotalQuantity() => _ingredients.Count;

    public List<Ingredient> GetAll() => _ingredients.Values.ToList();
    
    public bool ExistsByName(string name) => _ingredients.ContainsKey(name);

    public bool Consume(Ingredient ingredient)
    {
        if (!_ingredients.TryGetValue(ingredient.Name, out var storedIngredient)) 
            return false;
        
        var requestedQuantityInStoredUnit = ConvertUnit(
            ingredient.Quantity, 
            ingredient.Unit, 
            storedIngredient.Unit
        );
        
        if (storedIngredient.Quantity < requestedQuantityInStoredUnit) return false;
        
        storedIngredient.Quantity -= requestedQuantityInStoredUnit;
        _ingredients[ingredient.Name] = storedIngredient;
        return true;
    }

    public List<Ingredient> GetIngredientsByNames(IEnumerable<string> names)
    {
        return names
            .Where(name => _ingredients.ContainsKey(name))
            .Select(name => _ingredients[name])
            .ToList();    
    }
   
    private static decimal ConvertUnit(decimal quantity, Unit fromUnit, Unit toUnit)
    {
        if (fromUnit == toUnit) return quantity;
        
        var inBaseUnit = fromUnit switch
        {
            Unit.Kg => quantity * 1000m,    
            Unit.Grams => quantity,
            Unit.Liters => quantity * 1000m,
            Unit.Ml => quantity,
            _ => throw new InvalidOperationException($"Unknown unit: {toUnit}")
        };
        
        return toUnit switch
        {
            Unit.Kg => inBaseUnit / 1000m,     
            Unit.Grams => inBaseUnit,
            Unit.Liters => inBaseUnit / 1000m,
            Unit.Ml => inBaseUnit,
            _ => throw new InvalidOperationException($"Unknown unit: {toUnit}")
        };
    }
}