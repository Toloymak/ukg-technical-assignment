using UKG.HCM.Application.Services;

namespace UKG.HCM.WebApi.Services;

/// UserContextAccessor Accessor 
public interface IUserContextAccessorFactory
{
    /// Create a new instance of <see cref="IUserContextAccessor"/>
    IUserContextAccessor Create();
}

/// <inheritdoc />
internal class UserContextAccessorFactory : IUserContextAccessorFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextAccessorFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IUserContextAccessor Create()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity?.IsAuthenticated == true)
            return WebApiUserContextAccessor.CreateForService("anonymous");

        var personIdClaim = user.FindFirst("personId");

        if (personIdClaim == null || !Guid.TryParse(personIdClaim.Value, out var personGuid))
            return WebApiUserContextAccessor.CreateForService("missing_person_id");

        return WebApiUserContextAccessor.CreateForUser(personGuid);
    }
}