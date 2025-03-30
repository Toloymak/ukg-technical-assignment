using CommonContracts.Types;
using FluentAssertions;
using LanguageExt;
using UKG.HCM.Application.Services.People.Get;
using UKG.HCM.WebApi.Endpoints.People;

namespace UKG.HCM.WebApi.Tests.Endpoints;

[TestFixture]
public class GetPersonEndpointTests : BaseUnitTest
{
    private readonly Mock<IProvidePeople> _peopleProviderMock = new();

    [Test]
    public async Task Should_Return_Ok_When_Person_Exists()
    {
        // Arrange
        var expected = Fixture.Create<PersonDto>();
        var personId = expected.PersonId;

        _peopleProviderMock
            .Setup(p => p.GetPerson(personId, CancellationToken.None))
            .ReturnsAsync(Option<PersonDto>.Some(expected));

        // Act
        var result = await GetPersonEndpoint.GetPerson(
            personId, _peopleProviderMock.Object, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<Ok<PersonDto>>()
            .Which.Value.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task Should_Return_NotFound_When_Person_Does_Not_Exist()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();

        _peopleProviderMock
            .Setup(p => p.GetPerson(personId, CancellationToken.None))
            .ReturnsAsync(Option<PersonDto>.None);

        // Act
        var result = await GetPersonEndpoint.GetPerson(personId, _peopleProviderMock.Object, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFound>();
    }
}