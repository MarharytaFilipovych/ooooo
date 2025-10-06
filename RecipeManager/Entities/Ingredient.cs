namespace RecipeManager.Entities;

public class Ingredient
{
    public string Name { get; set; } = String.Empty;
    public decimal Quantity { get; set; }
    public Unit Unit { get; set; }
    
    public Ingredient() { }

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
        return $"{Name}, {Quantity} {Unit}";
    }
}