using JetBrains.Annotations;

namespace Contracts.Dtos.People;

[PublicAPI]
public sealed record CreatePersonRequest
{
    public required string? Email { get; init; }
    
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}