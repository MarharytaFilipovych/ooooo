namespace RecipeManager.Entities;

public class Recipe
{
    public string Name { get;  }
    public List<string> Steps { get; }
    public List<string> Ingredients { get; }

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

    public void AddIngredient(string ingredient)
    {
        if (!string.IsNullOrWhiteSpace(ingredient))
        {
            Ingredients.Add(ingredient);
        }
    }

    public override string ToString()
    {
        var result = new System.Text.StringBuilder();
        
        result.AppendLine($"Recipe: {Name}");
        result.AppendLine();
        
        result.AppendLine($"Ingredients ({Ingredients.Count}):");
        foreach (var ingredient in Ingredients)
        {
            result.AppendLine($"  - {ingredient}");
        }
        
        result.AppendLine();
        result.AppendLine($"Steps ({Steps.Count}):");
        for (int i = 0; i < Steps.Count; i++)
        {
            result.AppendLine($"  {i + 1}. {Steps[i]}");
        }
        
        return result.ToString();
    }

}

