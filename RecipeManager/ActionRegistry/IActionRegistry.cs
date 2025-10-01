using RecipeManager.Actions;

namespace RecipeManager.ActionRegistry;

public interface IActionRegistry
{
    void Register(IRecipeAction action);
    IRecipeAction? GetAction(string name);
    List<IRecipeAction> GetAllActions();
}