namespace UKG.HCM.Application.Services.Accounts;

internal class PasswordService : IPasswordService
{
    /// <inheritdoc />
    public (string hashedPassword, string salt) GenerateHashAndSalt(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return (hashedPassword, salt);
    }
}