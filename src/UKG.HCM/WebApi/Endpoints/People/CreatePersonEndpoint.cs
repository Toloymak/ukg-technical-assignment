using Application.BusinessServices.People.Create;
using Contracts.Dtos.People;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UKG.HCM.WebApi.Configuration.Swagger;
using UKG.HCM.WebApi.Extensions;
using UKG.HCM.WebApi.Mappers;

namespace UKG.HCM.WebApi.Endpoints.People;

public class CreatePersonEndpoint : IEndpointDefinition
{
    /// <inheritdoc />
    public static void Define(IEndpointRouteBuilder builder) => builder
        .MapGet("people/create", CreatePerson)
        .RequireAuthorizationPolicy(Policies.People.Create.Name)
        .WithTags(SwaggerTags.People)
        .WithDescription("Gets all people with pagination");

    internal static async Task<Results<Ok<string>, BadRequest<string>>> CreatePerson(
        [FromBody] CreatePersonRequest request,
        IMap<CreatePersonRequest, PersonCreateCommand> mapToCommand,
        ICreatePerson personCreator,
        CancellationToken ct = default)
    {
        var command = mapToCommand.Map(request);
        var result = await personCreator.Create(command, ct);

        return result.Match<Results<Ok<string>, BadRequest<string>>>(
            personId => TypedResults.Ok($"people/{personId}"),
            error => TypedResults.BadRequest(error.Message));
    }
}