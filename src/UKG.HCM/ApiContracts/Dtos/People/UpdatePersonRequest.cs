using JetBrains.Annotations;

namespace ApiContracts.Dtos.People;

[PublicAPI]
public sealed record UpdatePersonRequest
{
    public required string? Email { get; init; }
    
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}