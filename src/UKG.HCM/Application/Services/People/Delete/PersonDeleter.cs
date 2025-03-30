using LanguageExt;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.People.Delete;

/// <inheritdoc />
public class PersonDeleter : IDeletePerson
{
    private readonly IPeopleRepository _repository;

    public PersonDeleter(IPeopleRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Either<ErrorResult, Unit>> DeletePerson(
        Guid personId,
        CancellationToken ct)
    {
        await _repository.Delete(personId, ct);
        return Unit.Default;
    }
}