using RecipeManager.Entities;
using RecipeManager.Storage.IngredientStorage;
using RecipeManager.Storage.RecipeStorage;
using RecipeManager.Utils;

namespace RecipeManager.Storage.SessionStorage;


public class JsonSessionStorage : ISessionStorage
{
    private static readonly string DirectoryName = Path.Combine(GetProjectRoot(), "OurBeautifulUsers");    
    private readonly IIngredientStorage _ingredientStorage;
    private readonly IRecipeStorage _recipeStorage;
    private readonly IStorage<Plan> _planStorage;
    private User? _currentUser;
    
    public JsonSessionStorage(
        IIngredientStorage ingredientStorage, IRecipeStorage recipeStorage,
        IStorage<Plan> planStorage)
    {
        _ingredientStorage = ingredientStorage;
        _recipeStorage = recipeStorage;
        _planStorage = planStorage;

        if (!Directory.Exists(DirectoryName)) Directory.CreateDirectory(DirectoryName);
    }
    
   
    public bool TryLoad(string username, out User? user, out string? error)
    {
        user = null;
        error = null;
        
        ClearStorages();
        
        var filePath = GetUserFilePath(username);

        if (!File.Exists(filePath))
        {
            _currentUser = new User(username);
            user = _currentUser;
            return true;
        }

        try
        {
            var json = File.ReadAllText(filePath);
            var userData = JsonHelper.Deserialize<UserData>(json);
            
            if (userData == null || string.IsNullOrWhiteSpace(userData.Username))
            {
                error = $"We honestly don't know why, but something really " +
                                  $"strange happened to your data:(( You are {username}, by the way.";
                return false;
            }

            
            _ingredientStorage.AddAll(userData.Ingredients);
            _recipeStorage.AddAll(userData.Recipes);
            _planStorage.AddAll(userData.Plans);
            _currentUser = new User(username, userData.Subscription);
            user = _currentUser;
            return true;

        }
        catch (Exception ex)
        {
            error = "Sorry, beauty, but your data was not loaded, " +
                              $"that's a perfect time to start with a clean slate:): {ex.Message}";
            return false;
        }
    }

    public void Save()
    {
        try
        {
            var userData = CreateUserData();
            var json = JsonHelper.Serialize(userData);
            var filePath = GetUserFilePath(_currentUser!.Username);
            
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Sorry, darling, we could not save" +
                    $" your precious data:(((((: {ex.Message}");
        }
    }
    
    public bool UserExists(string username) => File.Exists(GetUserFilePath(username));
    
    public bool CurrentUserIsLoaded() => _currentUser is not null;
    
    public bool TryGetCurrentUser(out User user)
    {
        if (_currentUser is not null)
        {
            user = _currentUser;
            return true;
        }

        user = null!;
        return false;
    }

    private void ClearStorages()
    {
        _planStorage.RemoveAll();
        _recipeStorage.RemoveAll();
        _ingredientStorage.RemoveAll();
    }

    private UserData CreateUserData()
    {
        return new UserData
        {
            Username = _currentUser!.Username,
            Subscription = _currentUser.Subscription,
            Ingredients = _ingredientStorage.GetAll(),
            Recipes = _recipeStorage.GetAll(),
            Plans = _planStorage.GetAll()
        };
    }
    
    private static string GetUserFilePath(string username)
    {
        var cleanedUsername = string.Join("_", username.Split(Path.GetInvalidFileNameChars()));
        return Path.Combine(DirectoryName, $"{cleanedUsername}.json");
    }
    
    private static string GetProjectRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        var projectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        var csprojFile = $"{projectName}.csproj";

        while (directory != null && !File.Exists(Path.Combine(directory.FullName, csprojFile)))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? AppContext.BaseDirectory;
    }
}