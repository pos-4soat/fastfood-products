namespace fastfood_products.Domain.Contants;

public static class ErrorMessages
{
    public static Dictionary<string, string> ErrorMessageList => _errorMessages;

    private static readonly Dictionary<string, string> _errorMessages = new()
    {
        { "PBE001", "Request inválido ou fora de especificação, vide documentação" },
        { "PBE002", "O nome deve estar preenchido." },
        { "PBE003", "O nome deve ter no minimo 3 e no máximo 255 caracteres." },
        { "PBE004", "O tipo do produto deve estar preenchido." },
        { "PBE005", "O tipo do produto especificado não é válido." },
        { "PBE006", "O preço do produto deve estar especificado." },
        { "PBE007", "O descrição do produto deve estar preenchido." },
        { "PBE008", "A descrição do produto deve ter no minimo 3 e no máximo 255 caracteres." },
        { "PBE009", "A imagem do produto deve estar preenchida." },
        { "PBE010", "Produto não encontrado para os parâmetros informados." },
        { "PBE011", "O id deve ser informado." },
        { "PIE999", "Internal server error" }
    };
}
