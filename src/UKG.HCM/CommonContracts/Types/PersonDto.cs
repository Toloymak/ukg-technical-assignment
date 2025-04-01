namespace CommonContracts.Types;

// TODO: Move it to API contracts
public record PersonDto
{
    public required Guid PersonId { get; init; }
    
    public required string? Email { get; set; }
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}