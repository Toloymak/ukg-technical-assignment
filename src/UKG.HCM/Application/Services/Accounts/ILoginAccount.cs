using LanguageExt;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.Accounts;

public interface ILoginAccount
{
    // Login account
    Task<Either<ErrorResult, Account>> LoginAccount(
        string login, string password, CancellationToken ct);

}