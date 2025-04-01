namespace UKG.HCM.Application.Services.People.Create;

public record PersonCreateCommand
{
    public required string? Email { get; init; }
    
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}