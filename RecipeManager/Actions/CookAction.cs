using RecipeManager.Entities;
using System.Text;

namespace RecipeManager.Actions;

public class CookAction : IRecipeAction
{
    public string Name => "cook";
    public string Description => "Consume ingredients from pantry to cook the recipe";
    public bool RequiresParam => false;

    public ActionResult Execute(ActionContext context)
    {
        decimal multiplier = 1;

        if (!string.IsNullOrWhiteSpace(context.Parameter))
        {
            if (!decimal.TryParse(context.Parameter, out multiplier) || multiplier <= 0)
            {
                return ActionResult.Bad("Multiplier must be a positive number");
            }
        }

        var missingIngredients = new List<string>();
        var insufficientIngredients = new List<string>();

        foreach (var i in context.Recipe.Ingredients)
        {
            var inStock = context.IngredientStorage.GetEntityByName(i.Name);

            if (inStock == null)
            {
                missingIngredients.Add(i.Name);
            }
            else
            {
                var requiredQuantity = i.Quantity * multiplier;

                if (inStock.Unit != i.Unit)
                {
                    insufficientIngredients.Add($"{i.Name} units need {i.Unit}, have {inStock.Unit}");
                }
                else if (inStock.Quantity < requiredQuantity)
                {
                    insufficientIngredients.Add($"{i.Name} need {requiredQuantity} {i.Unit}, have {inStock.Quantity} {inStock.Unit}");
                }
            }
        }

        if (missingIngredients.Any() || insufficientIngredients.Any())
        {
            var result = new StringBuilder();
            result.AppendLine($"Cannot cook {context.Recipe.Name}");

            if (missingIngredients.Any())
            {
                result.AppendLine("Not in stock:");
                foreach (var i in missingIngredients)
                {
                    result.AppendLine($"  - {i}");
                }
            }

            if (insufficientIngredients.Any())
            {
                result.AppendLine("Insufficient quantity:");
                foreach (var i in insufficientIngredients)
                {
                    result.AppendLine($"  - {i}");
                }
            }

            return ActionResult.Bad(result.ToString());
        }

        foreach (var i in context.Recipe.Ingredients)
        {
            var ingredientToConsume = new Ingredient(
                i.Name,
                i.Quantity * multiplier,
                i.Unit
            );

            if (!context.IngredientStorage.Consume(ingredientToConsume))
            {
                return ActionResult.Bad($"Cannot consume {i.Name}");
            }
        }

        var success = multiplier == 1
            ? $"Successfully cooked {context.Recipe.Name}"
            : $"Successfully cooked {context.Recipe.Name} x{multiplier}";
        
        return ActionResult.Good(success);
    }
}