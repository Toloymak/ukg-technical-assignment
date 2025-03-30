using CommonContracts.Types;
using LanguageExt;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Persistence;

/// Work with person data
public interface IPeopleRepository
{
    /// Create a new person
    Task<Guid> Create(Person person, CancellationToken ct);
    
    /// Get a person by id
    // It's better to return domain model and map to DTO in the controller, but it's overengineered for that
    Task<Option<PersonDto>> GetById(Guid id, CancellationToken ct);
    
    /// Get list of people
    /// It's better to return domain model and map to DTO in the controller, but it's overengineered for that
    Task<Page<PersonDto>> GetList(int pageNumber, int pageSize, CancellationToken ct);
    
    /// Update a person
    Task<Either<NotFoundError, Unit>> Update(Person person, CancellationToken ct);
    
    /// Delete a person
    Task Delete(Guid personId, CancellationToken ct);

    /// Check if a person exists
    Task<bool> IsPersonExists(Guid id, CancellationToken ct);
}