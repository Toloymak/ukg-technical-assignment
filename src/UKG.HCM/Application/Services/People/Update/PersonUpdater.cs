using LanguageExt;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.People.Update;

/// <inheritdoc />
public class PersonUpdater : IUpdatePerson
{
    private readonly IPeopleRepository _peopleRepository;

    public PersonUpdater(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    /// <inheritdoc />
    public async Task<Either<ErrorResult, Unit>> Update(
        PersonUpdateCommand command,
        CancellationToken ct)
    {
        var person = new Person
        {
            PersonId = command.PersonId,
            Name = new Name()
            {
                FirstName = command.FirstName,
                LastName = command.LastName
            },
            Email = command.Email != null
                ? new Email(command.Email)
                : null,
        };
        
        var result = await _peopleRepository.Update(person, ct)
            .ToAsync()
            .MapLeft<ErrorResult>(x => x);
        
        return result;
    }
}