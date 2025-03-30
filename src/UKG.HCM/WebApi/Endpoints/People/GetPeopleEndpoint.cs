using System.ComponentModel.DataAnnotations;
using Application.BusinessServices.People.Get;
using CommonContracts.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UKG.HCM.WebApi.Configuration.Swagger;
using UKG.HCM.WebApi.Extensions;

namespace UKG.HCM.WebApi.Endpoints.People;

/// Get people
public class GetPeopleEndpoint : IEndpointDefinition
{
    /// <inheritdoc />
    public static void Define(IEndpointRouteBuilder builder) => builder
        .MapGet("people", GetPeople)
        .RequireAuthorizationPolicy(Policies.People.ReadAll.Name)
        .WithTags(SwaggerTags.People)
        .WithDescription("Gets all people with pagination");

    // TBD
    internal static async Task<Ok<Page<PersonDto>>> GetPeople(
        IProvidePeople peopleProvider,
        [FromQuery(Name = "page")] int pageNumber = 1,
        [FromQuery(Name = "size"), Range(1, 500)] int pageSize = 100,
        CancellationToken token = default)
    {
        var page = await peopleProvider.GetPeople(pageNumber, pageSize, token);
        return TypedResults.Ok(page);
    }
}