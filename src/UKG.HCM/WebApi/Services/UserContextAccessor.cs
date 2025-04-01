using LanguageExt;
using UKG.HCM.Application.Services;

namespace UKG.HCM.WebApi.Services;

/// WebApi implementation of <see cref="IUserContextAccessor"/>.
public class WebApiUserContextAccessor : IUserContextAccessor
{
    /// <inheritdoc />
    public Either<Guid, string> User { get; set; }

    /// Create a context based on a user ID
    public static WebApiUserContextAccessor CreateForUser(Guid userId)
        => new()
        {
            User = userId
        };

    /// Create a context based on a service name
    public static WebApiUserContextAccessor CreateForService(string serviceName)
        => new()
        {
            User = serviceName
        };
}