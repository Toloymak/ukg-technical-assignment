namespace UKG.HCM.WebApi.Extensions;

/// Route handler builder extensions
public static class RouteHandlerBuilderExtensions
{
    /// Require policy and produce status codes
    public static RouteHandlerBuilder RequireAuthorizationPolicy(
        this RouteHandlerBuilder builder, string policyName)
        => builder
                // TODO: Fix me!
            // .RequireAuthorization(policyName)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
}