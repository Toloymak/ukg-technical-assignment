namespace UKG.HCM.Application.Persistence;

public interface IActorsRepository
{
    /// Get or create actor id
    Task<Guid> GetOrCreateServiceActorId(
        string serviceName, CancellationToken ct);
    
    /// Get or create actor id
    Task<Guid> GetOrCreateServiceActorId(
        Guid personGuid, CancellationToken ct);
}