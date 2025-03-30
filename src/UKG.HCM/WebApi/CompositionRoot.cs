using ApiContracts.Dtos.People;
using UKG.HCM.Application;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Application.Services;
using UKG.HCM.Application.Services.People.Create;
using UKG.HCM.Application.Services.People.Update;
using UKG.HCM.Infrastructure;
using UKG.HCM.Infrastructure.Repositories;
using UKG.HCM.WebApi.Mappers;
using UKG.HCM.WebApi.Services;

namespace UKG.HCM.WebApi;

public static class CompositionRoot
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<UserContextAccessorFactory>();
        services.AddScoped<IUserContextAccessor>(sp =>
        {
            var factory = sp.GetRequiredService<UserContextAccessorFactory>();
            return factory.Create();
        });
        
        services
            .AddApplication()
            .AddInfrastructure();
        
        services
            .AddTransient<IMap<CreatePersonRequest, PersonCreateCommand>, PeopleMapper>()
            .AddTransient<IMapWithKey<Guid, UpdatePersonRequest, PersonUpdateCommand>, PeopleMapper>()
            ;
        
        
        return services;
    }
    
}