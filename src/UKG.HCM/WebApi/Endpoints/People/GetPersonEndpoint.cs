using System.ComponentModel.DataAnnotations;
using CommonContracts.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UKG.HCM.Application.Services.People.Get;
using UKG.HCM.WebApi.Configuration.Swagger;
using UKG.HCM.WebApi.Extensions;

namespace UKG.HCM.WebApi.Endpoints.People;

public class GetPersonEndpoint : IEndpointDefinition
{
    /// <inheritdoc />
    public static void Define(IEndpointRouteBuilder builder) => builder
        .MapGet("people/{personId:guid}", GetPerson)
        .RequireAuthorizationPolicy(Policies.People.ReadAll.Name)
        .WithTags(SwaggerTags.People)
        .WithDescription("Gets a person by ID");

    internal static async Task<Results<Ok<PersonDto>, NotFound>> GetPerson(
        [FromRoute, Required] Guid personId,
        IProvidePeople peopleProvider,
        CancellationToken ct)
    {
        var personOption = await peopleProvider.GetPerson(personId, ct);

        return personOption.Match<Results<Ok<PersonDto>, NotFound>>(
            person => TypedResults.Ok(person),
            () => TypedResults.NotFound());
    }
}