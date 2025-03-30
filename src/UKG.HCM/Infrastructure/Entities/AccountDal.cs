namespace Infrastructure.Entities;

public class AccountDal
{
    public Guid AccountId { get; set; }
    
    public required string Login { get; set; }
    
    public required string Password { get; set; }
    public required string PasswordSalt { get; set; }
    
    public required Guid PersonId { get; set; }
    public PersonDal? Person { get; set; }
}