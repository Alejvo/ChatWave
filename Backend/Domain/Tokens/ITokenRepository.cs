namespace Domain.Tokens;

public interface ITokenRepository
{
    Task<Token?> GetToken(string token);
    Task SaveToken(Token token);
}
