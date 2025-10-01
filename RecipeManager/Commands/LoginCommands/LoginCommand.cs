namespace RecipeManager.Commands.LoginCommands;

public class LoginCommand(string username) : ICommand
{
    public string Username { get; } = username;
}