using LanguageExt;
using Microsoft.Extensions.Logging;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.People.Update;

/// <inheritdoc />
public class PersonUpdaterEntityCheckDecorator : IUpdatePerson
{
    private readonly IUpdatePerson _decorated;
    private readonly IPeopleRepository _peopleRepository;
    private readonly ILogger<PersonUpdaterEntityCheckDecorator> _logger;

    public PersonUpdaterEntityCheckDecorator(
        IUpdatePerson decorated,
        IPeopleRepository peopleRepository,
        ILogger<PersonUpdaterEntityCheckDecorator> logger)
    {
        _decorated = decorated;
        _peopleRepository = peopleRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Either<ErrorResult, Unit>> Update(
        PersonUpdateCommand command,
        CancellationToken ct)
    {
        var isPersonExists = await _peopleRepository.IsPersonExists(command.PersonId, ct);
        if (!isPersonExists)
        {
            _logger.LogError("Person with ID {PersonId} not found", command.PersonId);
            return new ErrorResult("Person not found");
        }

        return await _decorated.Update(command, ct);
    }
}