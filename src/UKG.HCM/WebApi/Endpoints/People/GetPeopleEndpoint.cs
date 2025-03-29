using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UKG.HCM.WebApi.Configuration.PolicyNames;
using UKG.HCM.WebApi.Configuration.Swagger;
using UKG.HCM.WebApi.Extensions;

namespace UKG.HCM.WebApi.Endpoints.People;

/// Get people
public class GetPeopleEndpoint : IEndpointDefinition
{
    /// <inheritdoc />
    public static void Define(IEndpointRouteBuilder builder) => builder
        .MapGet("executions", GetExecutions)
        .RequireAuthorizationPolicy(Policies.People.ReadAll.Name)
        .WithTags(SwaggerTags.People)
        .WithDescription("Gets all people with pagination");

    // TBD
    public static async Task<Results<Ok<string[]>, BadRequest<string>>> GetExecutions(
        [FromQuery(Name = "page")] int pageNumber = 1,
        [FromQuery(Name = "size"), Range(1, 500)] int pageSize = 100,
        CancellationToken token = default)
    {
        await Task.CompletedTask;

        string[] results = ["Person 1", "Person 2", "Person 3"];
        return TypedResults.Ok(results);
    }
}