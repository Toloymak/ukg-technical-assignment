using CommonContracts.Types;
using LanguageExt;

namespace UKG.HCM.Application.Services.People.Get;

/// People provider
public interface IProvidePeople
{
    /// Get people with pagination
    // It's better to return domain model and map to DTO in the controller, but it's overengineered for that
    Task<Page<PersonDto>> GetPeople(int pageNumber, int pageSize, CancellationToken ct);
    
    /// Get person by id
    // It's better to return domain model and map to DTO in the controller, but it's overengineered for that
    Task<Option<PersonDto>> GetPerson(Guid personId, CancellationToken ct);
}