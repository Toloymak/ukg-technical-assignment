using Microsoft.EntityFrameworkCore;
using UKG.HCM.Infrastructure.Contexts;
using UKG.HCM.WebApi.Services;

namespace UKG.HCM.WebApi.Configuration;

public static class DbConfig
{
    /// Configures the database context for the application.
    public static void ConfigureDb(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlServer")));
    }
    
    /// Migrates the database to the latest version.
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await dbContext.Database.MigrateAsync();
    }
    
    /// Seeds test data into the database.
    public static async Task SeedTestData(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var seeder = scope.ServiceProvider.GetRequiredService<ISeedData>();

        await seeder.SeedTestData();
    }
}