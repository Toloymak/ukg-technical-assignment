using System.Reflection;
using UKG.HCM.WebApi.Endpoints;

namespace UKG.HCM.WebApi.Configuration;

public static class EndpointConfig
{
    /// Register all endpoints in the WebApi assembly
    public static void RegisterAllEndpoints(this WebApplication app)
    {
        var endpointDefinitionType = typeof(IEndpointDefinition);
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        var apiAssembly = typeof(EndpointConfig).Assembly;

        logger.LogDebug("Endpoints registration started for assembly {AssemblyName}",
            apiAssembly.GetName().Name);
        
        var types = apiAssembly
            .GetTypes()
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false }
                && endpointDefinitionType.IsAssignableFrom(type));

        foreach (var type in types)
        {
            CallEndpointDefineMethod(app, type, logger);
        }
        
        logger.LogDebug("Registered all endpoints");
    }

    private static void CallEndpointDefineMethod(
        IEndpointRouteBuilder builder,
        Type type,
        ILogger<Program> logger)
    {
        var method = type.GetMethod(nameof(IEndpointDefinition.Define),
            BindingFlags.Public | BindingFlags.Static);


        if (method == null)
            logger.LogError("No endpoint definition found for {Type}", type.Name);
        else
            method.Invoke(null, [builder]);
    }
}