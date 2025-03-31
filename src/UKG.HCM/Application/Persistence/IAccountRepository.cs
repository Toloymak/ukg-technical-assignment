using LanguageExt;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Persistence;

public interface IAccountRepository
{
    Task<Account?> GetAccountByLogin(
        string login,
        CancellationToken ct);

    Task<Account?> GetAccount(Guid accountId, CancellationToken ct);
    
    Task<Either<ErrorResult, Unit>> Update(Account account, CancellationToken ct);
}