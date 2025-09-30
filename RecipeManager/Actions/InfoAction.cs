using RecipeManager.Entities;

namespace RecipeManager.Actions;

public class InfoAction : IRecipeAction
{
    public string Name => "info";
    public string Description => "View recipe details";
    public bool RequiresParam => false;

    public ActionResult Execute(ActionContext context)
    {
        return ActionResult.Good(context.Recipe.ToString());
    }
}
