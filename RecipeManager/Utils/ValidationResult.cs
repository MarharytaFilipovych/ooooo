namespace RecipeManager.Utils;

public class ValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private ValidationResult(bool isValid, string? errorMessage = null)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static ValidationResult Success() => new(true);

    public static ValidationResult Failure(string errorMessage)
    {
        return string.IsNullOrWhiteSpace(errorMessage) 
            ? throw new ArgumentException("Error message cannot be empty")
            : new ValidationResult(false, errorMessage);
    }
}