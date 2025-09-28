namespace RecipeManager.Entities;

public class Ingredient
{
    public string Name { get; }
    public decimal Quantity { get; set; }
    public Unit Unit { get;  }
    
    public Ingredient(string name, decimal quantity, Unit unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name cannot be empty");
        Name = name.Trim();
        if(quantity < 0) 
            throw new ArgumentException("Quantity cannot be less than 0!");
        Quantity = quantity;
        Unit = unit;
    }

    public override string ToString()
    {
        return $"Ingredient {Name}, {Quantity} {Unit}";
    }
}