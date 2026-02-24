
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MindCarePro.Application.Dtos.Auth;
using MindCarePro.Application.Interfaces.Security;

namespace MindCarePro.Infrastructure.Security;


public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

   
    public TokenResult GenerateToken(Guid userId, string email)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var expireMinutes = double.Parse(jwtSettings["ExpireMinutes"]!);
        var expiration = DateTime.UtcNow.AddMinutes(expireMinutes);

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResult(tokenString, expiration);
    }

}
