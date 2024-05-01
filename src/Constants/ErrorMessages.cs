namespace fastfood_products.Constants;

public static class ErrorMessages
{
    public static Dictionary<string, string> ErrorMessageList => _errorMessages;

    private static readonly Dictionary<string, string> _errorMessages = new()
    {
        { "PBE001", "teste" },
        { "PBI999", "Internal server error" }
    };
}
