using Application.Errors;
using LanguageExt;

namespace Application.BusinessServices.People.Update;

/// Create a person
public interface IUpdatePerson
{
    /// Create a new person
    Task<Either<ErrorResult, Unit>> Update(
        PersonUpdateCommand command,
        CancellationToken cancellationToken);
}