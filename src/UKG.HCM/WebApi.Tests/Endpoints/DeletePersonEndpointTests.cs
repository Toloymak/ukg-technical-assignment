using FluentAssertions;
using LanguageExt;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Services.People.Delete;
using UKG.HCM.WebApi.Endpoints.People;

namespace UKG.HCM.WebApi.Tests.Endpoints;

public class DeletePersonEndpointTests: BaseUnitTest
{
    private readonly Mock<IDeletePerson> _deletePersonMock = new();

    [Test]
    public async Task Should_Return_NoContent_When_Delete_Succeeds()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        SetupDeletePerson(personId, Unit.Default);

        // Act
        var result = await DeletePersonEndpoint.DeletePerson(personId, _deletePersonMock.Object, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NoContent>();
        _deletePersonMock.Verify(d => d.DeletePerson(personId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Should_Return_NotFound_When_Person_Not_Found()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var error = new NotFoundError("person");
        SetupDeletePerson(personId, error);

        // Act
        var result = await DeletePersonEndpoint.DeletePerson(personId, _deletePersonMock.Object, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFound>();
        _deletePersonMock.Verify(d => d.DeletePerson(personId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Should_Return_Forbid_When_Forbidden()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var error = new ForbidError("you shall not pass");
        SetupDeletePerson(personId, error);

        // Act
        var result = await DeletePersonEndpoint.DeletePerson(personId, _deletePersonMock.Object, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ForbidHttpResult>();
        _deletePersonMock.Verify(d => d.DeletePerson(personId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Should_Return_BadRequest_When_Other_Error_Occurs()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var error = new ErrorResult("some other error");
        SetupDeletePerson(personId, error);

        // Act
        var result = await DeletePersonEndpoint.DeletePerson(personId, _deletePersonMock.Object, CancellationToken.None);

        // Assert
        var badRequest = result.Result.Should().BeOfType<BadRequest<string>>().Subject;
        badRequest.Value.Should().Be(error.Message);
        _deletePersonMock.Verify(d => d.DeletePerson(personId, It.IsAny<CancellationToken>()), Times.Once);
    }

    private void SetupDeletePerson(Guid personId, Either<ErrorResult, Unit> result)
    {
        _deletePersonMock
            .Setup(d => d.DeletePerson(personId, CancellationToken.None))
            .ReturnsAsync(result);
    }
}