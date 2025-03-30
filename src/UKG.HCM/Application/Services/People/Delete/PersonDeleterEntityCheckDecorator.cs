using LanguageExt;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.People.Delete;

/// <inheritdoc />
public class PersonDeleterEntityCheckDecorator : IDeletePerson
{
    private readonly IDeletePerson _service;
    private readonly IPeopleRepository _repository;
    private readonly ILogger<PersonDeleterEntityCheckDecorator> _logger;

    public PersonDeleterEntityCheckDecorator(
        IDeletePerson service,
        IPeopleRepository repository, ILogger<PersonDeleterEntityCheckDecorator> logger)
    {
        _service = service;
        _repository = repository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Either<ErrorResult, Unit>> DeletePerson(
        Guid personId,
        CancellationToken ct)
    {
        var isPersonExists = await _repository.IsPersonExists(personId, ct);
        if (!isPersonExists)
        {
            _logger.LogError("Error deleting person with id {PersonId}: {Error}", personId, "Person not found");
            return new NotFoundError("Person not found");
        }
        
        var result = await _service.DeletePerson(personId, ct);
        return result;
    }
}