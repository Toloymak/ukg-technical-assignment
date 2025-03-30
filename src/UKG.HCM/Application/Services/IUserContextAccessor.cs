using LanguageExt;

namespace UKG.HCM.Application.Services;

public interface IUserContextAccessor
{
    Either<Guid, string> User { get; set; }
}