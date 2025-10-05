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

        foreach (var i in context.Recipe.Ingredients)
        {
            var inStock = context.IngredientStorage.GetEntityByName(i.Name);

            if (inStock == null)
            {
                missingIngredients.Add(i.Name);
            }
            else if (inStock.Quantity == 0)
            {
                insufficientIngredients[i.Name] = $"have 0 {inStock.Unit}";
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

            if (!insufficientIngredients.Any()) return ActionResult.Good(result.ToString());
            
            result.AppendLine("\n Insufficient quantity:");
            foreach (var i in insufficientIngredients)
            {
                result.AppendLine($"  - {i.Key}: {i.Value}");
            }
        }

        return ActionResult.Good(result.ToString());
    }
}
