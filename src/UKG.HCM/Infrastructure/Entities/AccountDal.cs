namespace UKG.HCM.Infrastructure.Entities;

/// Account entity. Account - is a user of the system, that can be logged in.
public class AccountDal
{
    public Guid AccountId { get; set; }
    
    public string Login { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    
    public Guid PersonId { get; set; }
    public PersonDal? Person { get; set; }
    
    public ICollection<RoleDal> Roles { get; set; }
}