using CommonContracts.Types;
using LanguageExt;
using Microsoft.Extensions.Logging;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.People.Get;

/// <inheritdoc />
public class PeopleProvider : IProvidePeople
{
    private readonly IPeopleRepository _repository;
    private readonly ILogger<PeopleProvider> _logger;

    public PeopleProvider(IPeopleRepository repository, ILogger<PeopleProvider> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<Page<PersonDto>> GetPeople(
        int pageNumber, int pageSize, CancellationToken ct)
    {
        return _repository.GetList(pageNumber: pageNumber, pageSize: pageSize, ct);
    }

    /// <inheritdoc />
    public async Task<Option<PersonDto>> GetPerson(Guid personId, CancellationToken ct)
    {
        var person = await _repository.GetById(personId, ct);
        person.IfNone(() => _logger.LogWarning("Person with id {PersonId} not found", personId));
        
        return person;
    }
}