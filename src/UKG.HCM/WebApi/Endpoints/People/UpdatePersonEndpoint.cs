using System.ComponentModel.DataAnnotations;
using Application.BusinessServices.People.Update;
using Application.Errors;
using Contracts.Dtos.People;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UKG.HCM.WebApi.Configuration.Swagger;
using UKG.HCM.WebApi.Extensions;
using UKG.HCM.WebApi.Mappers;

namespace UKG.HCM.WebApi.Endpoints.People;

public class UpdatePersonEndpoint : IEndpointDefinition
{
    /// <inheritdoc />
    public static void Define(IEndpointRouteBuilder builder) => builder
        .MapPut("people/{personId:guid}", UpdatePerson)
        .RequireAuthorizationPolicy(Policies.People.Update.Name)
        .WithTags(SwaggerTags.People)
        .WithDescription("Updates an existing person by ID");

    internal static async Task<Results<Ok<string>, NotFound, BadRequest<string>>> UpdatePerson(
        [FromRoute, Required] Guid personId,
        [FromBody] UpdatePersonRequest request,
        IMapWithKey<Guid, UpdatePersonRequest, PersonUpdateCommand> mapper,
        IUpdatePerson updater,
        CancellationToken ct)
    {
        var command = mapper.Map(personId, request);
        var result = await updater.Update(command, ct);

        return result.Match<Results<Ok<string>, NotFound, BadRequest<string>>>(
            _ => TypedResults.Ok($"people/{personId}"),
            error => error switch
            {
                NotFoundError => TypedResults.NotFound(),
                InvalidDataError => TypedResults.BadRequest(error.Message),
                _ => TypedResults.BadRequest(error.Message)
            });
    }
}