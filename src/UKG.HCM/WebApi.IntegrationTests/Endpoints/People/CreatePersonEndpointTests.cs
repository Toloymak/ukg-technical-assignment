using System.Net;
using System.Net.Http.Json;
using ApiContracts.Dtos.People;
using AutoFixture;
using FluentAssertions;
using WebApi.IntegrationTests.Infrastructure;

namespace WebApi.IntegrationTests.Endpoints.People;

[TestFixture]
public class CreatePersonEndpointTests : BaseIntegrationTest
{
    [Test]
    public async Task Should_Create_Person()
    {
        // Arrange
        var client = GetClient();
        var request = new CreatePersonRequest
        {
            Email = $"{Guid.NewGuid()}@example.com",
            FirstName = Fixture.Create<string>(),
            LastName = Fixture.Create<string>()
        };

        var response = await client.PostAsJsonAsync("people/create", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseText = await response.Content.ReadAsStringAsync();
        
    }
}