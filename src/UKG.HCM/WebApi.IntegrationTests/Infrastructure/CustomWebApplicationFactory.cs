using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using UKG.HCM.Infrastructure.Contexts;

namespace WebApi.IntegrationTests.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private MsSqlContainer _container;
    private bool _isDisposedContainer;
    
    private const string MsSqlServer2022 = "mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04";
    private const string MsSqlServerPassword = "localdevpassword#123";

    public CustomWebApplicationFactory()
    {
        _container = new MsSqlBuilder()
            .WithPassword(MsSqlServerPassword)
            .WithImage(MsSqlServer2022)
            .WithName($"UKG_HCM_IntegrationTests_MsSql_{DateTime.Now.Ticks}")
            .WithCleanUp(true)
            .Build();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveDbContext(services);
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(_container.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
        => await _container.StartAsync();
    
    private void RemoveDbContext(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<AppDbContext>));

        if (dbContextDescriptor != null)
            services.Remove(dbContextDescriptor);
    }

    public override async ValueTask DisposeAsync()
    {
        if (_isDisposedContainer)
            return;
        
        await _container.DisposeAsync();
        await base.DisposeAsync();
        
        _isDisposedContainer = true;
        
        GC.SuppressFinalize(this);
    }
}