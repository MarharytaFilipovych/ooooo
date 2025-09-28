namespace RecipeManager.Entities;

public class Ingredient
{
    public Ingredient(int quantity, Unit unit, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name cannot be empty");
        Name = name.Trim();
        if(quantity < 0) 
            throw new ArgumentException("Quantity cannot be less than 0!");
        Quantity = quantity;
        Unit = unit;
    }

    public string Name { get; }
    public int Quantity { get; }
    public Unit Unit { get;  }
}