using LanguageExt;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Services.People;
using UKG.HCM.Application.Services.People.Create;
using UnitTests.Common.Extensions;

namespace UKG.HCM.Application.Tests.Services;

// TODO: Add more tests for all validaitons
[TestFixture]
public class PersonCreatorValidationDecoratorTests : BaseUnitTest
{
    private readonly Mock<ICreatePerson> _innerServiceMock = new();
    private readonly Mock<IPersonDataValidator> _validatorMock = new();

    [SetUp]
    public void Setup()
    {
        _innerServiceMock.Reset();
        _validatorMock.Reset();
    }
    
    [Test]
    public async Task Should_Return_Error_When_FirstName_Invalid()
    {
        // Arrange
        var command = Fixture.Create<PersonCreateCommand>();
        var error = new ErrorResult("First name error");

        _validatorMock.Setup(v => v.ValidateFirstName(command.FirstName)).Returns(error);

        var decorator = new PersonCreatorValidationDecorator(_innerServiceMock.Object, _validatorMock.Object);

        // Act
        var result = await decorator.Create(command, CancellationToken.None);

        // Assert
        result.ShouldBeLeft().Which.Should().BeEquivalentTo(error);
        _innerServiceMock.Verify(x => x.Create(It.IsAny<PersonCreateCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Should_Return_Error_When_LastName_Invalid()
    {
        // Arrange
        var command = Fixture.Create<PersonCreateCommand>();
        var error = new ErrorResult("Last name error");

        _validatorMock.Setup(v => v.ValidateFirstName(command.FirstName)).Returns(Unit.Default);
        _validatorMock.Setup(v => v.ValidateLastName(command.LastName)).Returns(error);

        var decorator = new PersonCreatorValidationDecorator(_innerServiceMock.Object, _validatorMock.Object);

        // Act
        var result = await decorator.Create(command, CancellationToken.None);

        // Assert
        result.ShouldBeLeft().Which.Should().BeEquivalentTo(error);
        _innerServiceMock.Verify(x => x.Create(It.IsAny<PersonCreateCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Should_Return_Error_When_Email_Invalid()
    {
        // Arrange
        var command = Fixture.Create<PersonCreateCommand>();
        var error = new ErrorResult("Email error");

        _validatorMock.Setup(v => v.ValidateFirstName(command.FirstName)).Returns(Unit.Default);
        _validatorMock.Setup(v => v.ValidateLastName(command.LastName)).Returns(Unit.Default);
        _validatorMock.Setup(v => v.ValidateEmail(command.Email)).Returns(error);

        var decorator = new PersonCreatorValidationDecorator(_innerServiceMock.Object, _validatorMock.Object);

        // Act
        var result = await decorator.Create(command, CancellationToken.None);

        // Assert
        result.ShouldBeLeft().Which.Should().BeEquivalentTo(error);
        _innerServiceMock.Verify(x => x.Create(It.IsAny<PersonCreateCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Should_Delegate_To_Inner_When_Validation_Passes()
    {
        // Arrange
        var command = Fixture.Create<PersonCreateCommand>();
        var expectedId = Guid.NewGuid();

        _validatorMock.Setup(v => v.ValidateFirstName(command.FirstName)).Returns(Unit.Default);
        _validatorMock.Setup(v => v.ValidateLastName(command.LastName)).Returns(Unit.Default);
        _validatorMock.Setup(v => v.ValidateEmail(command.Email)).Returns(Unit.Default);

        _innerServiceMock
            .Setup(x => x.Create(command, CancellationToken.None))
            .ReturnsAsync(expectedId);

        var decorator = new PersonCreatorValidationDecorator(_innerServiceMock.Object, _validatorMock.Object);

        // Act
        var result = await decorator.Create(command, CancellationToken.None);

        // Assert
        result.ShouldBeRight().Which.Should().Be(expectedId);
        _innerServiceMock.Verify(x => x.Create(command, CancellationToken.None), Times.Once);
    }
}