namespace Application.BusinessServices.People.Create;

public class PersonCreateCommand
{
    public required string? Email { get; set; }
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}