using RecipeManager.ActionRegistry;
using RecipeManager.Actions;
using RecipeManager.CommandDefinitions.ActionDefinitions;
using RecipeManager.Executors.ActionExecutors;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.EventPublishing;

namespace RecipeManager.CommandFactory;

public class ActionCommandSubFactory(
    IRecipeStorage recipeStorage, 
    IIngredientStorage ingredientStorage,
    IEventPublisher eventPublisher) : ICommandSubFactory
{
    public void Create(Context context)
    {
        var actionRegistry = new RecipeActionRegistry();
        
        actionRegistry.Register(new InfoAction());
        actionRegistry.Register(new ScaleAction());
        actionRegistry.Register(new MissingAction());
        actionRegistry.Register(new CookAction());
        
        context.Register(new OptionDefinition(), new OptionsExecutor(recipeStorage, actionRegistry));
        
        context.Register(new ActionDefinition(), new ActionsExecutor(recipeStorage, ingredientStorage, actionRegistry, eventPublisher));
    }
}