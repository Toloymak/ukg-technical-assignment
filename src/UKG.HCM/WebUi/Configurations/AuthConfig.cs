using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUi.Services;

namespace WebUi.Configurations;

public static class AuthConfig
{
    public static WebAssemblyHostBuilder RegisterAuth(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider>(
            provider => provider.GetRequiredService<AuthService>());

        builder.Services.AddScoped<AuthService>();
        
        return builder;
    }
}