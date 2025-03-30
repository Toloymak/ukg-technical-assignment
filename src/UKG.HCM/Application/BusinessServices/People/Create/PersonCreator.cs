using Application.Errors;
using LanguageExt;

namespace Application.BusinessServices.People.Create;

public class PersonCreator : ICreatePerson
{
    /// <inheritdoc />
    public Task<Either<ErrorResult, Guid>> Create(
        PersonCreateCommand command,
        CancellationToken cancellationToken)
    {
        // Simulate creating a person and returning a new Guid
        return Task.FromResult<Either<ErrorResult, Guid>>(Guid.NewGuid());
    }
}