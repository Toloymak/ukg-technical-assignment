namespace UKG.HCM.Application.Services.Accounts;

public interface IPasswordService
{
    /// Get hash and salt for password
    (string hashedPassword, string salt) GenerateHashAndSalt(string password);
}