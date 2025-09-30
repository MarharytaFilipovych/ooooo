namespace RecipeManager.Actions;

public class ActionResult
{
    public bool Success { get; }
    public string Message { get; }
    public object? Data { get; }

    private ActionResult(bool success, string message, object? data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ActionResult Good(string message, object? data = null) => new(true, message, data);

    public static ActionResult Bad(string message, object? data = null) => new(false, message, data);
}
