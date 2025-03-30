using Application.BusinessServices.People.Create;
using Application.Errors;
using Contracts.Dtos.People;
using FluentAssertions;
using UKG.HCM.WebApi.Endpoints.People;
using UKG.HCM.WebApi.Mappers;

namespace WebApi.Tests.Endpoints;

[TestFixture]
public class CreatePersonEndpointTests : BaseUnitTest
{
    private readonly Mock<IMap<CreatePersonRequest, PersonCreateCommand>> _mapMock = new();
    private readonly Mock<ICreatePerson> _createPersonMock = new();

    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public async Task Should_Return_Ok_When_Creation_Succeeds()
    {
        // Arrange
        var createdId = Fixture.Create<Guid>();
        var request = Fixture.Create<CreatePersonRequest>();
        var commandAfterMapping = Fixture.Create<PersonCreateCommand>();
        
        _mapMock.Setup(m => m.Map(request)).Returns(commandAfterMapping);
        _createPersonMock
            .Setup(c => c.Create(commandAfterMapping, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdId);

        // Act
        var result = await CreatePersonEndpoint.CreatePerson(
            request,
            _mapMock.Object,
            _createPersonMock.Object);

        // Assert
        var valueAssertion = result.Result.Should().BeOfType<Ok<string>>();
        valueAssertion.Which.Value.Should().Be($"people/{createdId}");
    }

    [Test]
    public async Task Should_Return_BadRequest_When_Creation_Fails()
    {
        // Arrange
        var request = Fixture.Create<CreatePersonRequest>();
        var commandAfterMapping = Fixture.Create<PersonCreateCommand>();
        var error = Fixture.Create<ErrorResult>();

        _mapMock.Setup(m => m.Map(request)).Returns(commandAfterMapping);

        _createPersonMock
            .Setup(c => c.Create(commandAfterMapping, It.IsAny<CancellationToken>()))
            .ReturnsAsync(error);

        // Act
        var result = await CreatePersonEndpoint.CreatePerson(
            request,
            _mapMock.Object,
            _createPersonMock.Object);

        // Assert
        var valueAssertion = result.Result.Should().BeOfType<BadRequest<string>>();
        valueAssertion.Which.Value.Should().Be(error.Message);
    }
}