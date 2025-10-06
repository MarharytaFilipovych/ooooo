namespace RecipeManager.Entities;

public class Plan
{
    public string Name { get; set; } = string.Empty;
    public string RecipeName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal ServingsMultiplier { get; set; } = 1;
    
    public Plan() { }

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
        var multiplierText = ServingsMultiplier == 1 ? "" : $" (x{ServingsMultiplier})";
        return $"{Name}: {RecipeName} {multiplierText} servings on {Date:yyyy-MM-dd}";
    }
}