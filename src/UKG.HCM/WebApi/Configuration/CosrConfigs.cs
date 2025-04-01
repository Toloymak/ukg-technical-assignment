namespace UKG.HCM.WebApi.Configuration;

public static class CosrConfigs
{
    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("UI", policy =>
            {
                policy
                    .WithOrigins("http://localhost:5361") // TODO: Use config instead
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}