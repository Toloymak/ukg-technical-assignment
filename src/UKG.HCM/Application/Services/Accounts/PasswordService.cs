namespace UKG.HCM.Application.Services.Accounts;

public interface IPasswordService
{
    (string hashedPassword, string salt) GenerateHashAndSalt(string password);
}

public class PasswordService : IPasswordService
{
    public (string hashedPassword, string salt) GenerateHashAndSalt(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return (hashedPassword, salt);
    }
}