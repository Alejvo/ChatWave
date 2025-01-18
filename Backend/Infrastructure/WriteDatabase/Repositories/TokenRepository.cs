using Dapper;
using Domain.Tokens;
using Infrastructure.WriteDatabase.Factories;
using System.Data;

namespace Infrastructure.WriteDatabase.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly SqlConnectionFactory _sqlConnection;

    public TokenRepository(SqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    public async Task<Token?> GetToken(string token)
    {
        using var connection = _sqlConnection.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Token>(
            TokenProcedures.GetToken.ToString(),
            new { token },
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveToken(Token token)
    {
        using var connection = _sqlConnection.CreateConnection();
        await connection.ExecuteAsync(
            TokenProcedures.SaveToken.ToString(),
            new { token.UserId, Token = token.JwtToken, token.Id, token.ExpiryTime },
            commandType: CommandType.StoredProcedure);
    }
}
