using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Application.Services.People.Create;
using UnitTests.Common.Extensions;

namespace UKG.HCM.Application.Tests.Services;

[TestFixture]
public class PersonCreatorTests : BaseUnitTest
{
    private readonly Mock<IPeopleRepository> _repositoryMock = new();

    [Test]
    public async Task Should_Return_Id_When_Person_Created()
    {
        // Arrange
        var command = Fixture.Create<PersonCreateCommand>();
        var createdId = Fixture.Create<Guid>();

        _repositoryMock
            .Setup(r => r.Create(It.IsAny<Person>(), CancellationToken.None))
            .ReturnsAsync(createdId);

        var creator = new PersonCreator(_repositoryMock.Object);

        // Act
        var result = await creator.Create(command, CancellationToken.None);

        // Assert
        result.ShouldBeRight()
            .Which.Should().Be(createdId);

        _repositoryMock.Verify(r =>
            r.Create(It.Is<Person>(p =>
                    p.Name.FirstName == command.FirstName &&
                    p.Name.LastName == command.LastName &&
                    p.Email != null && p.Email.Value == command.Email),
                CancellationToken.None), Times.Once);
    }
}