using RecipeManager.Actions;

namespace RecipeManager.ActionRegistry;

public class RecipeActionRegistry : IActionRegistry
{
    private readonly Dictionary<string, IRecipeAction> _actions = new(StringComparer.OrdinalIgnoreCase);

    public void Register(IRecipeAction action)
    {
        _actions[action.Name] = action;
    }

    public IRecipeAction? GetAction(string name)
    {
        _actions.TryGetValue(name, out var action);
        return action;
    }

    public List<IRecipeAction> GetAllActions()
    {
        return _actions.Values.ToList();
    }
}