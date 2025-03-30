using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.People.Create;

/// Create a person
public interface ICreatePerson
{
    /// Create a new person
    Task<Either<ErrorResult, Guid>> Create(
        PersonCreateCommand command,
        CancellationToken ct);
}