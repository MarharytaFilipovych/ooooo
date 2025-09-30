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

        var result = new StringBuilder();
        result.AppendLine($"Recipe {context.Recipe.Name} x{multiplier}");
        result.AppendLine("\n Ingredients: ");

        foreach (var i in context.Recipe.Ingredients)
        {
            var ingredient = context.IngredientStorage.GetEntityByName(i);
            if (ingredient != null)
            {
                var quantity = ingredient.Quantity * multiplier;
                result.AppendLine($"  - {i}: {quantity} {ingredient.Unit}");
            }
            else
            {
                result.AppendLine($"  - {i}: not in stock");
            }
        }

        return ActionResult.Good(result.ToString());
    }
}
