using Microsoft.Extensions.DependencyInjection;
using UKG.HCM.Application.Services.People;
using UKG.HCM.Application.Services.People.Create;
using UKG.HCM.Application.Services.People.Delete;
using UKG.HCM.Application.Services.People.Get;
using UKG.HCM.Application.Services.People.Update;

namespace UKG.HCM.Application;

public static class CompositionRoot
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddTransient<ICreatePerson, PersonCreator>()
            .Decorate<ICreatePerson, PersonCreatorValidationDecorator>()
            .AddTransient<IDeletePerson, PersonDeleter>()
            .Decorate<IDeletePerson, PersonDeleterEntityCheckDecorator>()
            .AddTransient<IProvidePeople, PeopleProvider>()
            .AddTransient<IUpdatePerson, PersonUpdater>()
            .Decorate<IUpdatePerson, PersonUpdaterEntityCheckDecorator>()
            .Decorate<IUpdatePerson, PersonUpdaterValidationDecorator>()
            .AddTransient<IPersonDataValidator, PersonDataValidator>()
            ;
        
        return services;
    }
    
}