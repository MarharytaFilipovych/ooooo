using RecipeManager.Entities;

namespace RecipeManager.Storage;

public class InMemoryUserStorage : IUserStorage
{
    private readonly Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);
    private User? _currentUser;

    public User GetOrCreateUser(string username)
    {
        if (_users.TryGetValue(username, out var existingUser))
        {
            return existingUser;
        }

        var newUser = new User(username);
        _users[username] = newUser;
        return newUser;
    }

    public User? GetCurrentUser()
    {
        return _currentUser;
    }
    
    public void SetCurrentUser(User user)
    {
        _currentUser = user;
    }
    
    public bool HasCurrentUser()
    {
        return _currentUser != null;
    }
}