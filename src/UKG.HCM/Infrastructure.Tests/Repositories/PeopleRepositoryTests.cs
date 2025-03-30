using AutoFixture;
using CommonContracts.Constants;
using FluentAssertions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Services;
using UKG.HCM.Infrastructure.Entities;
using UKG.HCM.Infrastructure.Repositories;
using UnitTests.Common.Extensions;

namespace UKG.HCM.Infrastructure.Tests.Repositories;

// TODO: Tests use the same service to create and remove the data. It's a bad idea, just a fast solution
[TestFixture]
public class PeopleRepositoryTests : TestBase
{
    private PeopleRepository _peopleRepository;
    private readonly Mock<ICurrentActorProvider> _currentActorProviderMock = new();
    private readonly Mock<IProvideCurrentDateTime> _dateTimeProviderMock = new();

    [SetUp]
    public void Setup()
    {
        _currentActorProviderMock.Reset();
        _currentActorProviderMock.Setup(x => x.GetActorGuid(CancellationToken.None))
            .ReturnsAsync(DefaultNames.AnonymousActorGuid);

        _dateTimeProviderMock.Reset();
        _dateTimeProviderMock.Setup(x => x.DateTimeOffsetNow())
            .Returns(DateTimeOffset.UtcNow);

        _peopleRepository = new PeopleRepository(
            DbContext,
            _currentActorProviderMock.Object,
            _dateTimeProviderMock.Object);
    }

    [Test]
    public async Task Should_Create_Entity_When_Call_Create()
    {
        // Arrange
        var person = Fixture.Create<Person>();

        // Act
        var personId = await _peopleRepository.Create(person, CancellationToken.None);

        // Assert
        var createdPerson = await DbContext.People
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PersonId == personId,
                CancellationToken.None);

        createdPerson.Should().NotBeNull();
        createdPerson.Should().BeEquivalentTo(person, options => options
            .ExcludingMissingMembers()
            .Excluding(x => x.PersonId));
    }

    [Test]
    public async Task Should_Return_Person_When_GetById_Called_With_Valid_Id()
    {
        // Arrange
        var person = Fixture.Create<Person>();
        var personId = await _peopleRepository.Create(person, CancellationToken.None);

        // Act
        var result = await _peopleRepository.GetById(personId, CancellationToken.None);

        // Assert
        var dto = result.ShouldBeSome();
        dto.Which.PersonId.Should().Be(personId);
        dto.Which.FirstName.Should().Be(person.Name.FirstName);
        dto.Which.LastName.Should().Be(person.Name.LastName);
        dto.Which.Email.Should().Be(person.Email?.Value);
    }

    [Test]
    public async Task Should_Return_None_When_GetById_Called_With_Invalid_Id()
    {
        // Act
        var result = await _peopleRepository.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.ShouldBeNone();
    }

    [Test]
    public async Task Should_Return_Paged_List_When_GetList_Called()
    {
        // Arrange
        var people = Fixture.CreateMany<Person>(15).ToList();
        foreach (var p in people)
            await _peopleRepository.Create(p, CancellationToken.None);

        // Act
        var page = await _peopleRepository.GetList(1, 10, CancellationToken.None);

        // Assert
        page.Items.Should().HaveCount(10);
        page.TotalCount.Should().Be(15);
    }

    [Test]
    public async Task Should_Update_Person_When_Exists()
    {
        // Arrange
        var person = Fixture.Create<Person>();
        var personId = await _peopleRepository.Create(person, CancellationToken.None);

        var updateModel = Fixture.Build<Person>()
            .With(x => x.PersonId, personId)
            .With(x => x.Name, Fixture.Create<Name>())
            .With(x => x.Email, Fixture.Create<Email>())
            .Create();

        // Act
        var result = await _peopleRepository.Update(updateModel, CancellationToken.None);

        // Assert
        result.ShouldBeRight();

        var entity = await DbContext.People.FirstAsync(x => x.PersonId == personId);
        entity.FirstName.Should().Be(updateModel.Name.FirstName);
        entity.LastName.Should().Be(updateModel.Name.LastName);
        entity.Email.Should().Be(updateModel.Email?.Value);
    }

    [Test]
    public async Task Should_Return_NotFound_When_Updating_Nonexistent_Person()
    {
        // Arrange
        var person = Fixture.Create<Person>();

        // Act
        var result = await _peopleRepository.Update(person, CancellationToken.None);

        // Assert
        result.ShouldBeLeft().Which.Should().BeOfType<NotFoundError>();
    }
    
    [Test]
    public async Task Should_Delete_Person_When_Exists()
    {
        // Arrange
        var person = Fixture.Create<Person>();
        var personId = await _peopleRepository.Create(person, CancellationToken.None);

        // Act
        await _peopleRepository.Delete(personId, CancellationToken.None);

        // Assert
        var result = await DbContext.People.CountAsync();
        result.Should().Be(0);
    }
    
    [Test]
    public async Task Should_Not_Throw_When_RemoveNotExising()
    {
        // Arrange
        var person = Fixture.Create<Person>();
        var personId = await _peopleRepository.Create(person, CancellationToken.None);

        // Act
        await _peopleRepository.Delete(personId, CancellationToken.None);

        // Assert
        var result = await DbContext.People.CountAsync();
        result.Should().Be(0);
    }
    
    [Test]
    public async Task Should_Return_True_When_Person_Exists()
    {
        // Arrange
        var person = Fixture.Create<Person>();
        var personId = await _peopleRepository.Create(person, CancellationToken.None);

        // Act
        var exists = await _peopleRepository.IsPersonExists(personId, CancellationToken.None);
        
        // Assert
        exists.Should().BeTrue();
    }

    [Test]
    public async Task Should_Return_False_When_Person_Does_Not_Exist()
    {
        // Act
        var exists = await _peopleRepository.IsPersonExists(Guid.NewGuid(), CancellationToken.None);
        
        // Assert
        exists.Should().BeFalse();
    }
}