using LanguageExt;
using UKG.HCM.Application.Services;

namespace UKG.HCM.WebApi.Services;

/// WebApi implementation of <see cref="IUserContextAccessor"/>.
public class WebApiUserContextAccessor : IUserContextAccessor
{
    /// <inheritdoc />
    public Either<Guid, string> User { get; set; }

    public static WebApiUserContextAccessor CreateForUser(Guid userId)
        => new()
        {
            User = userId
        };

    public static WebApiUserContextAccessor CreateForService(string serviceName)
        => new()
        {
            User = serviceName
        };
}