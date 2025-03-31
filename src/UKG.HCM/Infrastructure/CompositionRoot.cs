using Microsoft.Extensions.DependencyInjection;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Infrastructure.Repositories;

namespace UKG.HCM.Infrastructure;

public static class CompositionRoot
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddTransient<IPeopleRepository, PeopleRepository>()
            .AddTransient<IActorsRepository, ActorsRepository>()
            .AddTransient<IAccountRepository, AccountRepository>()
            ;
        
        return services;
    }
    
}