using LanguageExt;
using Microsoft.Extensions.Logging;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;

namespace UKG.HCM.Application.Services.Accounts;

public interface ILoginAccount
{
    // Login account
    Task<Either<ErrorResult, Account>> LoginAccount(
        string login, string password, CancellationToken ct);

}

/// Set password
public interface ISetPassword
{
    /// Set new password to account
    Task<Either<ErrorResult, Unit>> SetPassword(
        Guid accountId, string password, CancellationToken ct);
}

public class AccountManager : ILoginAccount, ISetPassword
{
    private readonly IAccountRepository _account;
    private readonly ILogger<AccountManager> _logger;
    private readonly IPasswordService _passwordService;

    public AccountManager(
        IAccountRepository account,
        ILogger<AccountManager> logger,
        IPasswordService passwordService)
    {
        _account = account;
        _logger = logger;
        _passwordService = passwordService;
    }

    public async Task<Either<ErrorResult, Account>> LoginAccount(string login, string password, CancellationToken ct)
    {
        var account = await _account.GetAccountByLogin(login, ct);

        if (account == null)
        {
            _logger.LogDebug("Login doesn't exists in the system");
            return new NotFoundError("person cannot be authenticated");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
        {
            _logger.LogDebug("Wrong password");
            return new NotFoundError("person cannot be authenticated");
        }

        return account;
    }

    public async Task<Either<ErrorResult, Unit>> SetPassword(Guid accountId, string password, CancellationToken ct)
    {
        var account = await _account.GetAccount(accountId, ct);

        if (account == null)
            return new NotFoundError("Account is not found");
        

        var (hashedPassword, salt) = _passwordService.GenerateHashAndSalt(password);

        account.SetPassword(salt, hashedPassword);

        await _account.Update(account, ct);
        return Unit.Default;
    }
}

