using UKG.HCM.Application.Persistence;
using UKG.HCM.Application.Services.People.Delete;

namespace UKG.HCM.Application.Tests.Services;

[TestFixture]
public class PersonDeleterTests : BaseUnitTest
{
    private readonly Mock<IPeopleRepository> _repositoryMock = new();

    [Test]
    public async Task Should_Delete_Person_And_Return_Unit()
    {
        // Arrange
        var personId = Fixture.Create<Guid>();

        _repositoryMock
            .Setup(r => r.Delete(personId, CancellationToken.None))
            .Returns(Task.CompletedTask);

        var deleter = new PersonDeleter(_repositoryMock.Object);

        // Act
        var result = await deleter.DeletePerson(personId, CancellationToken.None);

        // Assert
        result.ShouldBeRight();
        _repositoryMock.Verify(r => r.Delete(personId, CancellationToken.None), Times.Once);
    }
}