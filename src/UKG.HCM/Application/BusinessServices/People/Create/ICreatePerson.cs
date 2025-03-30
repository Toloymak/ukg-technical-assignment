using Application.Errors;
using LanguageExt;

namespace Application.BusinessServices.People.Create;

/// Create a person
public interface ICreatePerson
{
    /// Create a new person
    Task<Either<ErrorResult, Guid>> Create(
        PersonCreateCommand command,
        CancellationToken cancellationToken);
}