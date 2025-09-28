using RecipeManager.Entities;

namespace RecipeManager.Storage;

public interface IIngredientStorage : IStorage<Ingredient>
{
    bool Consume(Ingredient ingredient);
    List<Ingredient> GetIngredientsByNames(IEnumerable<string> names);
}