using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Maria.Rest.Infraestructure;

public static class AuthJwtExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string secretIdentity)
    {
        if (string.IsNullOrWhiteSpace(secretIdentity))
        {
            throw new ArgumentNullException("A variável MARIA_SECRET_IDENTITY não foi definida.");
        }

        var key = Encoding.ASCII.GetBytes(secretIdentity);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}