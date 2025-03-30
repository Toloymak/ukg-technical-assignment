namespace Infrastructure.Entities;

/// Person
public class PersonDal
{
    public Guid PersonId { get; init; }
    
    public required string? Email { get; set; }
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    // We can create people that cannot login, and don't need an account
    public required Guid? AccountId { get; set; }
    public AccountDal? Account { get; set; }
    
    public DateTimeOffset CreatedAt { get; init; }
    
    public Guid CreatedByActorId { get; init; }
    public ActionActorDal? CreatedByActor { get; init; }
}