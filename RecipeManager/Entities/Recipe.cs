namespace RecipeManager.Entities;

public class Recipe
{
    public string Name { get;  }
    public List<string> Steps { get; }
    public List<string> Ingredients { get; }

    public Recipe(string name, List<string> steps, List<string> ingredients)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Recipe name cannot be empty");
        Name = name.Trim();
        Steps = steps;
        Ingredients = ingredients;
    }
}

