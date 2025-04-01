using CommonContracts.Types;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Application.Services;
using UKG.HCM.Infrastructure.Contexts;
using UKG.HCM.Infrastructure.Entities;
using UKG.HCM.Infrastructure.Extensions;

namespace UKG.HCM.Infrastructure.Repositories;

/// <inheritdoc />
public class PeopleRepository : IPeopleRepository
{
    private readonly AppDbContext _context;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IProvideCurrentDateTime _dateTimeProvider;

    public PeopleRepository(
        AppDbContext context,
        ICurrentActorProvider currentActorProvider,
        IProvideCurrentDateTime dateTimeProvider)
    {
        _context = context;
        _currentActorProvider = currentActorProvider;
        _dateTimeProvider = dateTimeProvider;
    }

    /// <inheritdoc />
    public async Task<Guid> Create(Person person, CancellationToken ct)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        
        var dal = new PersonDal
        {
            Email = person.Email?.Value,
            FirstName = person.Name.FirstName,
            LastName = person.Name.LastName,
            CreatedByActorId = await _currentActorProvider.GetActorGuid(ct),
            AccountId = null,
            CreatedAt = _dateTimeProvider.DateTimeOffsetNow()
        };

        _context.People.Add(dal);
        await _context.SaveChangesAsync(ct);
        
        await transaction.CommitAsync(ct);

        return dal.PersonId;
    }

    /// <inheritdoc />
    public async Task<Option<PersonDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _context.People
            .Where(x => x.PersonId == id)
            .Select(x => new PersonDto
            {
                // Todo: map to person, it isn't Infrastructure responsibility to know about PersonDto
                PersonId = x.PersonId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
            })
            .FirstOrDefaultAsync(ct);
        
        return result == null ? Option<PersonDto>.None : Option<PersonDto>.Some(result);
    }

    /// <inheritdoc />
    public Task<Page<PersonDto>> GetList(int pageNumber, int pageSize, CancellationToken ct)
    {
        var query = _context.People
            .Select(x => new PersonDto
            {
                PersonId = x.PersonId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
            });

        return query.ToPage(pageNumber, pageSize, ct);
    }

    /// <inheritdoc />
    public async Task<Either<NotFoundError, Unit>> Update(
        Person person,
        CancellationToken ct)
    {
        // TODO: App ModifyBy field
        var dal = await _context.People
            .Where(dal => dal.PersonId == person.PersonId)
            .FirstOrDefaultAsync(ct);
        
        if (dal == null)
            return new NotFoundError($"Person with id {person.PersonId} not found");
        
        dal.FirstName = person.Name.FirstName;
        dal.LastName = person.Name.LastName;
        dal.Email = person.Email?.Value;

        await _context.SaveChangesAsync(ct);
        return Unit.Default;
    }

    /// <inheritdoc />
    public Task Delete(Guid personId, CancellationToken ct)
    {
        return _context.People
            .Where(x => x.PersonId == personId)
            .ExecuteDeleteAsync(ct);
    }

    /// <inheritdoc />
    public Task<bool> IsPersonExists(Guid id, CancellationToken ct)
        => _context.People
            .AnyAsync(x => x.PersonId == id, ct);
}