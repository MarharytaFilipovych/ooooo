using System.Text;
using RecipeManager.Entities;

namespace RecipeManager.Actions;

public class ScaleAction : IRecipeAction
{
    public string Name => "scale";
    public string Description => "Preview scaled ingredient quantities";
    public bool RequiresParam => true;

    public ActionResult Execute(ActionContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Parameter))
        {
            return ActionResult.Bad("Multiplier parameter is missing!");
        }

        if (!decimal.TryParse(context.Parameter, out var multiplier) || multiplier <= 0)
        {
            return ActionResult.Bad("Multiplier must be a positive number!");
        }

        if (multiplier == 0)
        {
            return ActionResult.Bad("Multiplier cannot be zero!");
        }

        var result = new StringBuilder();
        result.AppendLine($"Recipe {context.Recipe.Name} x{multiplier}");
        result.AppendLine("\n Ingredients: ");

        foreach (var i in context.Recipe.Ingredients)
        {
            var quantity = i.Quantity * multiplier;
            result.AppendLine($"  - {i}: {quantity} {i.Unit}");
        }

        return ActionResult.Good(result.ToString());
    }
}
