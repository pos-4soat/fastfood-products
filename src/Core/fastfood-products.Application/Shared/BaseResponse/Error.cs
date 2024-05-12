using fastfood_products.Domain.Contants;

namespace fastfood_products.Application.Shared.BaseResponse;

/// <param name="ErrorCode"> Código de erro. </param>
/// <param name="Message"> Mensagens de erro. </param>
public sealed record Error(string ErrorCode, string Message)
{
    public Error(string errorCode) : this(errorCode, ErrorMessages.ErrorMessageList[errorCode])
    {
        if (string.IsNullOrEmpty(Message))
            Message = ErrorMessages.ErrorMessageList["PIE999"];
    }
}