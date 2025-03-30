using Microsoft.AspNetCore.Mvc;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Services.People.Delete;
using UKG.HCM.WebApi.Configuration.Swagger;
using UKG.HCM.WebApi.Extensions;
using DeletePersonResult = Microsoft.AspNetCore.Http.HttpResults.Results<
    Microsoft.AspNetCore.Http.HttpResults.NoContent,
    Microsoft.AspNetCore.Http.HttpResults.NotFound,
    Microsoft.AspNetCore.Http.HttpResults.ForbidHttpResult,
    Microsoft.AspNetCore.Http.HttpResults.BadRequest<string>>;

namespace UKG.HCM.WebApi.Endpoints.People;

public class DeletePersonEndpoint : IEndpointDefinition
{
    /// <inheritdoc />
    public static void Define(IEndpointRouteBuilder builder) => builder
        .MapDelete("people/{personId:guid}", DeletePerson)
        .RequireAuthorizationPolicy(Policies.People.Delete.Name)
        .WithTags(SwaggerTags.People)
        .WithDescription("Gets all people with pagination");

    /// Delete person endpoint
    internal static async Task<DeletePersonResult> DeletePerson(
        [FromRoute] Guid personId,
        IDeletePerson deletePerson,
        CancellationToken ct)
    {
        var deleteResult = await deletePerson.DeletePerson(personId, ct);

        return deleteResult.Match<DeletePersonResult>(
            _ => TypedResults.NoContent(),
            error => error switch
            {
                NotFoundError => TypedResults.NotFound(),
                ForbidError => TypedResults.Forbid(),
                _ => TypedResults.BadRequest(error.Message)
            });
    }
}