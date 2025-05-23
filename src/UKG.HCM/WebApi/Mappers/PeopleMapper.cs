using ApiContracts.Dtos.People;
using UKG.HCM.Application.Services.People.Create;
using UKG.HCM.Application.Services.People.Update;

namespace UKG.HCM.WebApi.Mappers;

public class PeopleMapper :
    IMap<CreatePersonRequest, PersonCreateCommand>,
    IMapWithKey<Guid, UpdatePersonRequest, PersonUpdateCommand>
{
    /// <inheritdoc />
    public PersonCreateCommand Map(CreatePersonRequest source)
    {
        return new PersonCreateCommand()
        {
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName
        };
    }

    /// <inheritdoc />
    public PersonUpdateCommand Map(Guid key, UpdatePersonRequest source)
    {
        return new PersonUpdateCommand()
        {
            PersonId = key,
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName
        };
    }
}