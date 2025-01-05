using Application.Abstractions;
using Domain.Tokens;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Users.Services;

public class AuthService : IAuthService
{

    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(ITokenRepository tokenRepository, IUserRepository userRepository, IConfiguration configuration)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public string GenerateRefreshToken()
    {
        var byteArray = new byte[64];
        string refreshToken;
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(byteArray);
        refreshToken = Convert.ToBase64String(byteArray);
        return refreshToken;
    }

    public string GenerateToken(string userId, string username)
    {
        var key = _configuration.GetValue<string>("JwtSettings:Key");
        var keyBytes = Encoding.ASCII.GetBytes(key!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub,userId),
                    new Claim(JwtRegisteredClaimNames.UniqueName,username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task SaveRefreshToken(string userId, string refreshToken)
    {
        var user = await _userRepository.GetById( userId );
        if (user != null)
        {
            var token = new Token
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpiryTime = DateTime.UtcNow.AddMinutes(15),
                JwtToken = refreshToken
            };
            await _tokenRepository.SaveToken(token);
        }
    }
}
