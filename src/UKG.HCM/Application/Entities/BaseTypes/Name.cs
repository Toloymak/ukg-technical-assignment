namespace UKG.HCM.Application.Entities.BaseTypes;

public sealed record Name 
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}