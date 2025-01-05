namespace Application.Abstractions;

public interface IAuthService
{
    string GenerateToken(string userId, string username);
    string GenerateRefreshToken();
    Task SaveRefreshToken(string userId, string refreshToken);
}
