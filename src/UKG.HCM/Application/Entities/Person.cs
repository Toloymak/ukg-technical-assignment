namespace UKG.HCM.Application.Entities;

public class Person
{
    public required Guid PersonId { get; set; }
    
    public required Name Name { get; set; }
    public required Email? Email { get; set; }
}



public sealed record Name 
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}

public sealed record Email(string Value);