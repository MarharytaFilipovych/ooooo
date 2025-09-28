using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryPlanStorage : IStorage<Plan>
{
    private static readonly Dictionary<string, Plan> Plans = new();

    public bool Add(Plan entity)
    {
        return Plans.TryAdd(entity.Name, entity);
    }

    public bool Remove(string name)
    {
        return Plans.Remove(name);
    }

    public bool Update(Plan entity)
    {
        if (!Plans.ContainsKey(entity.Name)) return false;
        Plans[entity.Name] = entity;
        return true;

    }

    public Plan? GetEntityByName(string name)
    {
        Plans.TryGetValue(name, out var plan);
        return plan;
    }

    public int GetTotalQuantity()
    {
        return Plans.Count;
    }

    public List<Plan> GetAll()
    {
        return Plans.Values.ToList();
    }
}