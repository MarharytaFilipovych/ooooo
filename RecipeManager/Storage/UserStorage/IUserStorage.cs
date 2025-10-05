using RecipeManager.Entities;

namespace RecipeManager.Storage.UserStorage;

public interface IUserStorage
{
    User GetOrCreateUser(string username);
    User? GetCurrentUser();
    void SetCurrentUser(User user);
    bool HasCurrentUser();
}