using LanguageExt;
using Microsoft.Extensions.Logging;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Application.Services.People.Delete;
using UnitTests.Common.Extensions;

namespace UKG.HCM.Application.Tests.Services;

[TestFixture]
public class PersonDeleterEntityCheckDecoratorTests : BaseUnitTest
{
    private readonly Mock<IDeletePerson> _innerServiceMock = new();
    private readonly Mock<IPeopleRepository> _repositoryMock = new();
    private readonly Mock<ILogger<PersonDeleterEntityCheckDecorator>> _loggerMock = new();
    
    [SetUp]
    public void SetUp()
    {
        _innerServiceMock.Reset();
        _repositoryMock.Reset();
        _loggerMock.Reset();
    }

    [Test]
    public async Task Should_Return_NotFound_When_Person_Does_Not_Exist()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();

        _repositoryMock
            .Setup(r => r.IsPersonExists(personId, CancellationToken.None))
            .ReturnsAsync(false);

        var decorator = new PersonDeleterEntityCheckDecorator(
            _innerServiceMock.Object,
            _repositoryMock.Object,
            _loggerMock.Object);

        // Act
        var result = await decorator.DeletePerson(personId, CancellationToken.None);

        // Assert
        result.ShouldBeLeft().Which.Should().BeOfType<NotFoundError>();
        _innerServiceMock.Verify(s => s.DeletePerson(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Should_Delegate_To_Inner_When_Person_Exists()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var expected = Unit.Default;

        _repositoryMock
            .Setup(r => r.IsPersonExists(personId, CancellationToken.None))
            .ReturnsAsync(true);

        _innerServiceMock
            .Setup(s => s.DeletePerson(personId, CancellationToken.None))
            .ReturnsAsync(expected);

        var decorator = new PersonDeleterEntityCheckDecorator(
            _innerServiceMock.Object,
            _repositoryMock.Object,
            _loggerMock.Object);

        // Act
        var result = await decorator.DeletePerson(personId, CancellationToken.None);

        // Assert
        result.ShouldBeRight().Which.Should().Be(Unit.Default);
        _innerServiceMock.Verify(s => s.DeletePerson(personId, CancellationToken.None), Times.Once);
    }
}