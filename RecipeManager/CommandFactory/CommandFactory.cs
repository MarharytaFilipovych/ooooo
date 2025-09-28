using RecipeManager.Storage;

namespace RecipeManager.CommandFactory;

public static class CommandFactory
{
    public static Context Create()
    {
        var ingredientStorage = new InMemoryIngredientStorage();
        var recipeStorage = new InMemoryIngredientStorage();
        var planStorage = new InMemoryPlanStorage();
        
        var context = new Context();

        return context;
    }
}