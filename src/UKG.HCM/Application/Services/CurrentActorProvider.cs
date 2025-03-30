using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services;

/// Provides the current actor Id
public interface ICurrentActorProvider
{
    /// Provides the current actor's ID, which can be either a person or a service.
    Task<Guid> GetActorGuid(CancellationToken ct);
}

internal class CurrentActorProvider : ICurrentActorProvider
{
    private readonly IUserContextAccessor _accessor;
    private readonly IActorsRepository _actorsesRepository;

    public CurrentActorProvider(
        IUserContextAccessor accessor,
        IActorsRepository actorsesRepository)
    {
        _accessor = accessor;
        _actorsesRepository = actorsesRepository;
    }
    
    public Task<Guid> GetActorGuid(CancellationToken ct)
    {
        return _accessor.User
            .MatchAsync(
                serviceName => _actorsesRepository.GetOrCreateServiceActorId(serviceName, ct),
                personId => _actorsesRepository.GetOrCreateServiceActorId(personId, ct));
    }
}