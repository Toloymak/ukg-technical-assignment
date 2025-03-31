using System.Net;
using System.Net.Http.Json;
using ApiContracts.Dtos.People;
using AutoFixture;
using FluentAssertions;
using WebApi.IntegrationTests.Infrastructure;

namespace WebApi.IntegrationTests.Endpoints.People;

[TestFixture]
public class CreateAndRemovePersonTests : BaseIntegrationTest
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
        var resourcePath = (await response.Content.ReadAsStringAsync()).Trim('"');

        var isExistPerson = await IsExistPerson(client, resourcePath);
        isExistPerson.Should().BeTrue();
        
        // Act
        var deleteResult = await client.DeleteAsync(resourcePath);
        deleteResult.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        // Assert
        var isDeletedPerson = await IsExistPerson(client, resourcePath);
        isDeletedPerson.Should().BeFalse();
    }
    
    private async Task<bool> IsExistPerson(HttpClient client, string resourceUrl)
    {
        var response = await client.GetAsync(resourceUrl);
        return response.StatusCode == HttpStatusCode.OK;
    }
}