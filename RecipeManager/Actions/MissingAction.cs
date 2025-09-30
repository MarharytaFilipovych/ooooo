using System.Text;

namespace RecipeManager.Actions;

public class MissingAction : IRecipeAction
{
    public string Name => "missing";
    public string Description => "List ingredients missing from pantry";
    public bool RequiresParam => false;

    public ActionResult Execute(ActionContext context)
    {
        var missingIngredients = new List<string>();
        var insufficientIngredients = new Dictionary<string, string>();

        foreach (var name in context.Recipe.Ingredients)
        {
            var inStock = context.IngredientStorage.GetEntityByName(name);

            if (inStock == null)
            {
                missingIngredients.Add(name);
            }
            else if (inStock.Quantity == 0)
            {
                insufficientIngredients[name] = $"have 0 {inStock.Unit}";
            }
        }

        var result = new StringBuilder();
        result.AppendLine($"Missing ingredients for {context.Recipe.Name}");

        if (missingIngredients.Count == 0 && insufficientIngredients.Count == 0)
        {
            result.AppendLine("All ingredients in stock");
        }
        else
        {
            if (missingIngredients.Any())
            {
                result.AppendLine("\n Not in stock:");
                foreach (var m in missingIngredients)
                {
                    result.AppendLine($"  - {m}");
                }
            }

            if (insufficientIngredients.Any())
            {
                result.AppendLine("\n Insufficient quantity:");
                foreach (var i in insufficientIngredients)
                {
                    result.AppendLine($"  - {i}");
                }
            }
        }

        return ActionResult.Good(result.ToString());
    }
}
