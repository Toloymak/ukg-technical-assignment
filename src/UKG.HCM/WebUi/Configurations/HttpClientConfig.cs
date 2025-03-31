using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WebUi.Configurations;

public static class HttpClientConfiguration
{
    public static WebAssemblyHostBuilder RegisterHttpClient(this WebAssemblyHostBuilder builder)
    {
        var mainApiUrl = builder.Configuration["MainApi"];
        if(string.IsNullOrWhiteSpace(mainApiUrl))
            throw new InvalidOperationException("BaseUrl is not configured in appsettings.json");

        builder.Services.AddScoped(_ => new HttpClient
        {
            BaseAddress = new Uri(mainApiUrl)
        });

        return builder;
    }
}