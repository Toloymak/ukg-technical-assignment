using CommonContracts.Types;
using FluentAssertions;
using UKG.HCM.Application.Services.People.Get;
using UKG.HCM.WebApi.Endpoints.People;

namespace UKG.HCM.WebApi.Tests.Endpoints;

[TestFixture]
public class GetPeopleEndpointTests : BaseUnitTest
{
    private readonly Mock<IProvidePeople> _peopleProviderMock = new();

    [Test]
    public async Task Should_Return_Ok_With_People_Page()
    {
        // Arrange
        var expectedPage = Fixture.Create<Page<PersonDto>>();
        var pageNumber = 1;
        var pageSize = 20;

        _peopleProviderMock
            .Setup(p => p.GetPeople(pageNumber, pageSize, CancellationToken.None))
            .ReturnsAsync(expectedPage);

        // Act
        var result = await GetPeopleEndpoint.GetPeople(
            _peopleProviderMock.Object,
            pageNumber,
            pageSize,
            CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<Page<PersonDto>>>()
            .Which.Value.Should().BeEquivalentTo(expectedPage);
    }
}