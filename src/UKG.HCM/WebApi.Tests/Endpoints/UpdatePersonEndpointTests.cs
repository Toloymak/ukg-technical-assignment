using ApiContracts.Dtos.People;
using FluentAssertions;
using LanguageExt;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Services.People.Update;
using UKG.HCM.WebApi.Endpoints.People;
using UKG.HCM.WebApi.Mappers;

namespace UKG.HCM.WebApi.Tests.Endpoints;

[TestFixture]
public class UpdatePersonEndpointTests : BaseUnitTest
{
    private readonly Mock<IMapWithKey<Guid, UpdatePersonRequest, PersonUpdateCommand>> _mapperMock = new();
    private readonly Mock<IUpdatePerson> _updatePersonMock = new();

    [Test]
    public async Task Should_Return_Ok_When_Update_Succeeds()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var request = Fixture.Create<UpdatePersonRequest>();
        var command = Fixture.Create<PersonUpdateCommand>();
        var updatedId = personId;

        _mapperMock.Setup(m => m.Map(personId, request)).Returns(command);
        SetupUpdatePerson(command, Unit.Default);

        // Act
        var result = await UpdatePersonEndpoint.UpdatePerson(
            personId,
            request,
            _mapperMock.Object,
            _updatePersonMock.Object,
            CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<Ok<string>>()
            .Which.Value.Should().Be($"people/{updatedId}");

        _updatePersonMock.Verify(u => u.Update(command, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task Should_Return_NotFound_When_Person_Not_Found()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var request = Fixture.Create<UpdatePersonRequest>();
        var command = Fixture.Create<PersonUpdateCommand>();
        var error = new NotFoundError("person");

        _mapperMock.Setup(m => m.Map(personId, request)).Returns(command);
        SetupUpdatePerson(command, error);

        // Act
        var result = await UpdatePersonEndpoint.UpdatePerson(
            personId,
            request,
            _mapperMock.Object,
            _updatePersonMock.Object,
            CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFound>();
        _updatePersonMock.Verify(u => u.Update(command, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task Should_Return_BadRequest_When_InvalidData()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var request = Fixture.Create<UpdatePersonRequest>();
        var command = Fixture.Create<PersonUpdateCommand>();
        var error = new InvalidDataError("Invalid input");

        _mapperMock.Setup(m => m.Map(personId, request)).Returns(command);
        SetupUpdatePerson(command, error);

        // Act
        var result = await UpdatePersonEndpoint.UpdatePerson(
            personId,
            request,
            _mapperMock.Object,
            _updatePersonMock.Object,
            CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<BadRequest<string>>()
            .Which.Value.Should().Be(error.Message);

        _updatePersonMock.Verify(u => u.Update(command, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task Should_Return_BadRequest_When_Other_Error_Occurs()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();
        var request = Fixture.Create<UpdatePersonRequest>();
        var command = Fixture.Create<PersonUpdateCommand>();
        var error = new ErrorResult("unexpected");

        _mapperMock.Setup(m => m.Map(personId, request)).Returns(command);
        SetupUpdatePerson(command, error);

        // Act
        var result = await UpdatePersonEndpoint.UpdatePerson(
            personId,
            request,
            _mapperMock.Object,
            _updatePersonMock.Object,
            CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<BadRequest<string>>()
            .Which.Value.Should().Be(error.Message);

        _updatePersonMock.Verify(u => u.Update(command, CancellationToken.None), Times.Once);
    }

    private void SetupUpdatePerson(PersonUpdateCommand command, Either<ErrorResult, Unit> result)
    {
        _updatePersonMock
            .Setup(u => u.Update(command, CancellationToken.None))
            .ReturnsAsync(result);
    }
}