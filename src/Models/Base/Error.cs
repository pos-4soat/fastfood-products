namespace fastfood_products.Models.Base;

public class Error
{
    public Error() { }
    public Error(string errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }

    /// <summary>
    /// Código de erro.
    /// </summary>
    /// <example>XXX001</example>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Mensagens de erro.
    /// </summary>
    /// <example>Ocorreu um erro interno durante a chamada da api.</example>
    public string Message { get; set; }
}
