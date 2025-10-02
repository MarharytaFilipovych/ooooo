namespace RecipeManager.Entities;

public class User
{
    public string Username { get; }

    public User(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Recipe name cannot be empty");
        Username = username.Trim();
    }

    public override string ToString()
    {
        return $"User: {Username}";
    }
}