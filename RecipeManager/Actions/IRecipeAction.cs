namespace RecipeManager.Actions;

public interface IRecipeAction
{
    string Name { get; }
    string Description { get; }
    bool RequiresParam { get; }
    ActionResult Execute(ActionContext context);
}
