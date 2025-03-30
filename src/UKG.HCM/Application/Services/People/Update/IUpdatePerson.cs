using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.People.Update;

/// Create a person
public interface IUpdatePerson
{
    /// Create a new person
    Task<Either<ErrorResult, Unit>> Update(
        PersonUpdateCommand command,
        CancellationToken ct);
}