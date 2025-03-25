using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using meeplematch_api.DTO;
using meeplematch_api.Utils;
using Microsoft.IdentityModel.Tokens;

namespace meeplematch_api.Security;

public static class TokenUtils
{
    public static string GenerateToken(string username, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtEncryptionKey));
        var credentials = new SigningCredentials(securityKey, Constants.SecurityAlgorithm);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            null,
            null,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}