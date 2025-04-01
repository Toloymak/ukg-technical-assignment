using ApiContracts.Dtos.People;
using UKG.HCM.Application;
using UKG.HCM.Application.Services;
using UKG.HCM.Application.Services.People.Create;
using UKG.HCM.Application.Services.People.Update;
using UKG.HCM.Infrastructure;
using UKG.HCM.WebApi.Endpoints.Login;
using UKG.HCM.WebApi.Mappers;
using UKG.HCM.WebApi.Models;
using UKG.HCM.WebApi.Services;

namespace UKG.HCM.WebApi;

public static class CompositionRoot
{
    public static void AddWebApiDependencies(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextAccessorFactory, UserContextAccessorFactory>();
        services.AddScoped<IUserContextAccessor>(sp =>
        {
            var factory = sp.GetRequiredService<IUserContextAccessorFactory>();
            return factory.Create();
        });
        
        services
            .AddApplication()
            .AddInfrastructure();
        
        services
            .AddTransient<IMap<CreatePersonRequest, PersonCreateCommand>, PeopleMapper>()
            .AddTransient<IMapWithKey<Guid, UpdatePersonRequest, PersonUpdateCommand>, PeopleMapper>()
            .AddTransient<ISeedData, DataSeeder>()
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            ;
    }
}