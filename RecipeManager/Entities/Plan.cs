namespace RecipeManager.Entities;

public class Plan
{
    public string Name { get; }
    public string RecipeName { get;  }
    public DateTime Date { get;  }
    public decimal ServingsMultiplier { get; }

    public Plan(string name, string recipeName, DateTime date, decimal? servingsMultiplier)
    { 
        
        if (string.IsNullOrWhiteSpace(recipeName) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Names cannot be empty");
        Name = name;
        RecipeName = recipeName;
        Date = date;
        ServingsMultiplier = servingsMultiplier ?? 1;
        if (servingsMultiplier <= 0)
            throw new ArgumentException("Servings multiplier must be greater than 0!");
    }

    public override string ToString()
    {
        return $"Plan {Name}: recipe {RecipeName}, " +
               $"date: {Date}, {ServingsMultiplier} servings";
    }
}