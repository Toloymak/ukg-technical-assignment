using System.Security.Claims;
using WebUi.Models;

namespace WebUi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static CurrentUser ToCurrentUser(this ClaimsPrincipal user)
    {
        var current = new CurrentUser();

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userId, out var id))
            current.UserId = id;

        current.Login = user.FindFirst(ClaimTypes.Name)?.Value ?? "";
        current.FirstName = user.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
        current.LastName = user.FindFirst(ClaimTypes.Surname)?.Value ?? "";
        current.Roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        return current;
    }
}