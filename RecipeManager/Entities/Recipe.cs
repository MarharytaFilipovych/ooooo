using System.Text;

namespace RecipeManager.Entities;

public class Recipe
{
    public string Name { get;  }
    public List<string> Steps { get; }
    public List<Ingredient> Ingredients { get; }

    public Recipe(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Recipe name cannot be empty");
        Name = name.Trim();
        Steps = [];
        Ingredients = [];
    }

    public void AddSteps(string stepText)
    {
        if (!string.IsNullOrWhiteSpace(stepText))
        {
            Steps.Add(stepText);
        }
    }

    public void AddIngredient(Ingredient? ingredient)
    {
        if (ingredient != null)
        {
            Ingredients.Add(ingredient);
        }
    }

    public override string ToString()
    {
        var result = new StringBuilder();
    
        result.AppendLine($"Recipe: {Name}");
    
        switch (Ingredients.Count)
        {
            case 0 when Steps.Count == 0:
                result.AppendLine("No ingredients or steps added yet.");
                return result.ToString();
            case > 0:
            {
                result.AppendLine();
                result.AppendLine($"Ingredients ({Ingredients.Count}):");
                foreach (var ingredient in Ingredients)
                {
                    result.AppendLine($"  - {ingredient}");
                }
                break;
            }
        }
        
        if (Steps.Count <= 0) return result.ToString();
        
        result.AppendLine();
        result.AppendLine($"Steps ({Steps.Count}):");
        for (var i = 0; i < Steps.Count; i++)
        {
            result.AppendLine($"  {i + 1}. {Steps[i]}");
        }
        
        return result.ToString();
    }

}

