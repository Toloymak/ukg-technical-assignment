using NSwag;
using NSwag.Generation.Processors.Security;

namespace UKG.HCM.WebApi.Configuration;

/// OpenApi configuration
public static class SwaggerConfig
{
    /// Configures Swagger for the application.
    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOpenApi()
            .AddEndpointsApiExplorer()
            .AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1";
                config.Title = "UKG.HCM";
                
                config.AddSecurity("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    In = OpenApiSecurityApiKeyLocation.Header
                });
                
                config.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });

        return builder;
    }
}