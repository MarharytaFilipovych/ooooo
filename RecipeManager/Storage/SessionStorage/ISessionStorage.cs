using RecipeManager.Entities;

namespace RecipeManager.Storage.SessionStorage;

public interface ISessionStorage
{
    void Save();
    bool TryLoad(string username, out User? user, out string? error);
    bool CurrentUserIsLoaded();
    bool TryGetCurrentUser(out User? user);
}