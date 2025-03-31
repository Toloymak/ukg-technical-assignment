using System.Net;
using System.Net.Http.Json;
using ApiContracts.Dtos.People;
using AutoFixture;
using CommonContracts.Types;
using FluentAssertions;
using WebApi.IntegrationTests.Infrastructure;

namespace WebApi.IntegrationTests.Endpoints.People;

[TestFixture]
public class CreateAndGetPersonTests : BaseIntegrationTest
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

        // Act
        var response = await client.PostAsJsonAsync("people/create", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseText = await response.Content.ReadAsStringAsync();
        
        var responseFromReturnedPath = await client.GetAsync(responseText.Trim('"'));

        responseFromReturnedPath.StatusCode.Should().Be(HttpStatusCode.OK);
        var createdPerson = await responseFromReturnedPath.Content.ReadFromJsonAsync<PersonDto>();

        createdPerson.Should().NotBeNull();
        createdPerson.Email.Should().Be(request.Email);
        createdPerson.FirstName.Should().Be(request.FirstName);
        createdPerson.LastName.Should().Be(request.LastName);
    }
}