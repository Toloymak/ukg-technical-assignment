using Microsoft.EntityFrameworkCore;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Infrastructure.Contexts;
using UKG.HCM.Infrastructure.Entities;

namespace UKG.HCM.Infrastructure.Repositories;

/// <inheritdoc />
public class ActorsRepository : IActorsRepository
{
    private readonly AppDbContext _context;
    
    public ActorsRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Guid> GetOrCreateServiceActorId(string serviceName, CancellationToken ct)
    {
        var actor = await _context.ActionActors
            .Where(x => x.Service == serviceName)
            .Select(x => new { x.ActorId })
            .FirstOrDefaultAsync(ct);

        if (actor != null)
            return actor.ActorId;

        var newActor = new ActionActorDal { Service = serviceName };
        _context.ActionActors.Add(newActor);
        
        await _context.SaveChangesAsync(ct);

        return newActor.ActorId;
    }

    /// <inheritdoc />
    public async Task<Guid> GetOrCreateServiceActorId(Guid personGuid, CancellationToken ct)
    {
        var actor = await _context.ActionActors
            .Where(x => x.PersonId == personGuid)
            .Select(x => new { x.ActorId })
            .FirstOrDefaultAsync(ct);

        if (actor != null)
            return actor.ActorId;

        var newActor = new ActionActorDal { PersonId = personGuid };
        _context.ActionActors.Add(newActor);
        
        await _context.SaveChangesAsync(ct);

        return newActor.ActorId;
    }
}