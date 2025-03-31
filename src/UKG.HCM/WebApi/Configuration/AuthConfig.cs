using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UKG.HCM.WebApi.Configuration;

public static class AuthConfig
{
    public static void AddAuthenticationAndAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        var signingKey = builder.Configuration.GetSection("Jwt")["SigningKey"] ??
                        throw new InvalidOperationException("SigningKey is required!");;

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(signingKey))
                };
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
            });

        builder.ConfigurePolicies();
    }

    private static void ConfigurePolicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(Policies.People.Create.Name, policy => policy.RequireRole(Policies.People.Create.Roles))
            .AddPolicy(Policies.People.ReadAll.Name, policy => policy.RequireRole(Policies.People.ReadAll.Roles))
            .AddPolicy(Policies.People.Update.Name, policy => policy.RequireRole(Policies.People.Update.Roles))
            .AddPolicy(Policies.People.Delete.Name, policy => policy.RequireRole(Policies.People.Delete.Roles))
            ;
    }
}