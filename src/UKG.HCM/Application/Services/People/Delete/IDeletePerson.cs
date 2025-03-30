using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.People.Delete;

/// Delete a person
public interface IDeletePerson
{ 
    /// Delete a person by Id
    Task<Either<ErrorResult, Unit>> DeletePerson(
        Guid personId,
        CancellationToken ct);
}