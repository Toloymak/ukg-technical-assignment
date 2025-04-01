using LanguageExt;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Entities.BaseTypes;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.People.Create;

public class PersonCreator : ICreatePerson
{
    private readonly IPeopleRepository _repository;

    public PersonCreator(IPeopleRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Either<ErrorResult, Guid>> Create(
        PersonCreateCommand command,
        CancellationToken ct)
    {
        var person = new Person
        {
            PersonId = Guid.Empty,
            Name = new Name()
            {
                FirstName = command.FirstName,
                LastName = command.LastName
            },
            Email = command.Email != null
                ? new Email(command.Email)
                : null,
        };
        
        return await _repository.Create(person, ct);
    }
}