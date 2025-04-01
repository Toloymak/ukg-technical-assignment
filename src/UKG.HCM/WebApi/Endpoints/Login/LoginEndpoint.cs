using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UKG.HCM.Application.Services.Accounts;
using UKG.HCM.WebApi.Services;

namespace UKG.HCM.WebApi.Endpoints.Login;

public class LoginEndpoint : IEndpointDefinition
{
    public static void Define(IEndpointRouteBuilder builder)
        => builder.MapPost("auth/login", LogIn)
            .AllowAnonymous()
            .WithTags("Auth")
            .WithDescription("Login and receive a JWT token.");
    
    private static async Task<Results<Ok<AuthResponse>, UnauthorizedHttpResult>> LogIn(
        [FromBody] LogInRequest request,
        ILoginAccount accounts,
        IJwtTokenGenerator tokenGenerator,
        CancellationToken ct)
    {
        return await accounts.LoginAccount(request.Login, request.Password, ct)
            .ToAsync()
            .Map(tokenGenerator.Generate)
            .Match<Results<Ok<AuthResponse>, UnauthorizedHttpResult>>(
                token => TypedResults.Ok(new AuthResponse(token)),
                _ => TypedResults.Unauthorized()
            );
    }
}

public record LogInRequest(string Login, string Password);
public record AuthResponse(string AccessToken);