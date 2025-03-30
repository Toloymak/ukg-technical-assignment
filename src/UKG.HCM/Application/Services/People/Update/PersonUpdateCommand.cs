namespace UKG.HCM.Application.Services.People.Update;

public record PersonUpdateCommand
{
    public required Guid PersonId { get; init; }
    
    public required string? Email { get; init; }
    
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}