using RecipeManager.Entities;

namespace RecipeManager.Storage.PlanStorage;

public class InMemoryPlanStorage : IStorage<Plan>
{
    private readonly Dictionary<string, Plan> _plans = new(StringComparer.OrdinalIgnoreCase);

    public bool Add(Plan entity) => _plans.TryAdd(entity.Name, entity);

    public bool Remove(string name) => _plans.Remove(name);

    public bool Update(Plan entity)
    {
        if (!_plans.ContainsKey(entity.Name)) return false;
        _plans[entity.Name] = entity;
        return true;
    }

    public Plan? GetEntityByName(string name)
    {
        _plans.TryGetValue(name, out var plan);
        return plan;
    }

    public int GetTotalQuantity() => _plans.Count;

    public List<Plan> GetAll() => _plans.Values.ToList();
    
    public bool ExistsByName(string name) => _plans.ContainsKey(name);
    
    public void AddAll(IEnumerable<Plan> plans)
    {
        foreach (var plan in plans)
        {
            _plans.TryAdd(plan.Name, plan);
        }
    }

    public void RemoveAll()
    {
        _plans.Clear();
    }
}