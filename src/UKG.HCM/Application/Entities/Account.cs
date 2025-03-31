
namespace UKG.HCM.Application.Entities;

public class Account
{
    public required Guid AccountId { get; init; }
    
    public required string Login { get; set; }
    
    public required string PasswordHash { get; set; } = string.Empty;
    public required string PasswordSalt { get; set; } = string.Empty;
    
    public required HashSet<string> Roles { get; set; }
    
    public required Person Person { get; set; }

    public void SetPassword(string salt, string hashedPassword)
    {
        PasswordSalt = salt;
        PasswordHash = hashedPassword;
    }
}