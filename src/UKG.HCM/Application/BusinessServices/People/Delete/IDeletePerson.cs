using Application.Errors;
using LanguageExt;

namespace Application.BusinessServices.People.Delete;

public interface IDeletePerson
{ 
    Task<Either<ErrorResult, Unit>> DeletePerson(
        Guid personId,
        CancellationToken cancellationToken);
}