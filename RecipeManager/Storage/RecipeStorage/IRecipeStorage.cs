using RecipeManager.Entities;

namespace RecipeManager.Storage.RecipeStorage;

public interface IRecipeStorage : IStorage<Recipe>
{
    List<Recipe> GetRecipesByNames(IEnumerable<string> names);
}