using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Maria.Application.Accounts;

public interface ITokenService
{
    string GenerateJwt(ClaimsIdentity claimsIdentity);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GenerateJwt(ClaimsIdentity claimsIdentity)
    {
        var key = GetKey();
        var expireInSeconds = _configuration.GetSection("Configuration:ExpireInSeconds").Get<int>();

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.Add(new TimeSpan(0, 0, expireInSeconds)),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private byte[] GetKey()
    {
        var RDC_SECRET_IDENTITY = _configuration.GetSection("MARIA_SECRET_IDENTITY").Get<string>();
        if (string.IsNullOrWhiteSpace(RDC_SECRET_IDENTITY))
        {
            throw new ArgumentNullException("A variável MARIA_SECRET_IDENTITY não foi definida.");
        }
        return Encoding.ASCII.GetBytes(RDC_SECRET_IDENTITY);
    }
}
