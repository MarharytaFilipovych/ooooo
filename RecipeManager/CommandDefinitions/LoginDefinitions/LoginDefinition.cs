using RecipeManager.Commands;
using RecipeManager.Commands.LoginCommands;

namespace RecipeManager.CommandDefinitions.LoginDefinitions;

public class LoginDefinition : ICommandDefinition
{
    public string Name => "login";
    public string Description => "login <username>";

    public bool TryParse(string[] args, out ICommand? command, out string? error)
    {
        error = null;
        command = null;

        if (args.Length != 2)
        {
            error = $"Wrong usage, bro: {Description}";
            return false;
        }

        var username = args[1];
        if (string.IsNullOrWhiteSpace(username))
        {
            error = "Username cannot be empty!";
            return false;
        }

        command = new LoginCommand(username);
        return true;
    }
}