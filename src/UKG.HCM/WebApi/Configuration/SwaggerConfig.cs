namespace UKG.HCM.WebApi.Configuration;

/// OpenApi configuration
public static class SwaggerConfig
{
    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOpenApi()
            .AddEndpointsApiExplorer()
            .AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1";
                config.Title = "UKG.HCM";
            });

        return builder;
    }
}