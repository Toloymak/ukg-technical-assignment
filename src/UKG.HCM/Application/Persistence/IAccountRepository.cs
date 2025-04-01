using LanguageExt;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Persistence;

public interface IAccountRepository
{
    /// Get Account by login
    Task<Account?> GetAccountByLogin(
        string login,
        CancellationToken ct);

    /// Get Account by account id
    Task<Account?> GetAccount(Guid accountId, CancellationToken ct);
    
    /// Update Account
    Task<Either<ErrorResult, Unit>> Update(Account account, CancellationToken ct);
}