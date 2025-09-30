using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryPlanStorage : IStorage<Plan>
{
    private static readonly Dictionary<string, Plan> Plans = new(StringComparer.OrdinalIgnoreCase);

    public bool Add(Plan entity) => Plans.TryAdd(entity.Name, entity);

    public bool Remove(string name) => Plans.Remove(name);

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

    public int GetTotalQuantity() => Plans.Count;

    public List<Plan> GetAll() => Plans.Values.ToList();
    public bool ExistsByName(string name) => Plans.ContainsKey(name);
}