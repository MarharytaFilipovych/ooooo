using RecipeManager.Entities;

namespace RecipeManager.Storage;

public interface IRecipeStorage : IStorage<Recipe>
{
    List<Recipe> GetRecipesByNames(IEnumerable<string> names);
}