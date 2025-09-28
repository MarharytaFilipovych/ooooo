namespace RecipeManager.Entities;

public class Recipe
{
    public string Name { get;  }
    private List<string> Steps { get; }
    private List<string> Ingredients { get; }

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
        return $"Recipe {Name}:\n {Steps.Count} steps:\n {string.Join(", ", Steps)}\n " +
               $" {Ingredients.Count} ingredients:\n {string.Join(", ", Ingredients)}";
    }
}

