using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cwk.Application.Interfaces;
using Cwk.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cwk.Application.Services;

public class TokenService : ITokenService
{
    private readonly string? secretKey;

    public TokenService(IConfiguration configuration)
    {
         secretKey = configuration.GetSection("Jwt")
        .GetValue<string>("key");
        
    }

    public string GenerateToken(User user)
    {
       var keyBytes =Encoding.ASCII.GetBytes(secretKey!);
       var claims = new ClaimsIdentity();
       claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
       claims.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
       claims.AddClaim(new Claim("Nombre",  user.Name));

       var tokenDescriptor = new SecurityTokenDescriptor
       {
           Subject = claims,
           Expires = DateTime.UtcNow.AddMinutes(30),
           SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
           SecurityAlgorithms.HmacSha256)
       };

       var tokenHandler = new JwtSecurityTokenHandler();
       var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
       string tokenCreado = tokenHandler.WriteToken(tokenConfig);
        
       return tokenCreado;
    }
}