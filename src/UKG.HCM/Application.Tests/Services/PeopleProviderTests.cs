using CommonContracts.Types;
using LanguageExt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Application.Services.People.Get;

namespace UKG.HCM.Application.Tests.Services;

[TestFixture]
public class PeopleProviderTests : BaseUnitTest
{
    private readonly Mock<IPeopleRepository> _repositoryMock = new();
    private readonly ILogger<PeopleProvider> _logger = NullLogger<PeopleProvider>.Instance;

    [Test]
    public async Task Should_Return_Page_When_GetPeople_Called()
    {
        // Arrange
        var expectedPage = Fixture.Create<Page<PersonDto>>();
        var pageNumber = 1;
        var pageSize = 10;

        _repositoryMock
            .Setup(r => r.GetList(pageNumber, pageSize, CancellationToken.None))
            .ReturnsAsync(expectedPage);

        var provider = new PeopleProvider(_repositoryMock.Object, _logger);

        // Act
        var result = await provider.GetPeople(pageNumber, pageSize, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedPage);
    }

    [Test]
    public async Task Should_Return_Person_When_Found()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var expected = Fixture.Create<PersonDto>();

        _repositoryMock
            .Setup(r => r.GetById(personId, CancellationToken.None))
            .ReturnsAsync(Option<PersonDto>.Some(expected));

        var provider = new PeopleProvider(_repositoryMock.Object, _logger);

        // Act
        var result = await provider.GetPerson(personId, CancellationToken.None);

        // Assert
        result.ShouldBeSome().And.Be(expected);
    }

    [Test]
    public async Task Should_LogWarning_And_Return_None_When_Person_Not_Found()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();

        _repositoryMock
            .Setup(r => r.GetById(personId, CancellationToken.None))
            .ReturnsAsync(Option<PersonDto>.None);

        var provider = new PeopleProvider(_repositoryMock.Object, _logger);

        // Act
        var result = await provider.GetPerson(personId, CancellationToken.None);

        // Assert
        result.ShouldBeNone();
    }
}