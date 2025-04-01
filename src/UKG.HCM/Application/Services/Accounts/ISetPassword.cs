using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.Accounts;

/// Set password
public interface ISetPassword
{
    /// Set new password to account
    Task<Either<ErrorResult, Unit>> SetPassword(
        Guid accountId, string password, CancellationToken ct);
}