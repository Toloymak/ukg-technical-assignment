using CommonContracts.Constants;
using LanguageExt;
using UKG.HCM.Application.Services.People;

namespace UKG.HCM.Application.Tests.Services;

[TestFixture]
public class PersonDataValidatorTests : BaseUnitTest
{
    private readonly PersonDataValidator _validator = new();

    [Test]
    public void Should_Return_Error_When_FirstName_Is_Null_Or_Whitespace()
    {
        var result = _validator.ValidateFirstName(" ");
        result.ShouldBeLeft().Which.Message.Should().Be("First name is required");
    }

    [Test]
    public void Should_Return_Error_When_FirstName_Too_Long()
    {
        var result = _validator.ValidateFirstName(new string('A', DataConstraints.MaxFirstNameLength + 1));
        result.ShouldBeLeft().Which.Message.Should().Contain("First name must be less than");
    }

    [Test]
    public void Should_Return_Unit_When_FirstName_Is_Valid()
    {
        var result = _validator.ValidateFirstName("John");
        result.ShouldBeRight().Which.Should().Be(Unit.Default);
    }

    [Test]
    public void Should_Return_Error_When_LastName_Is_Null_Or_Whitespace()
    {
        var result = _validator.ValidateLastName(" ");
        result.ShouldBeLeft().Which.Message.Should().Be("Last name is required");
    }

    [Test]
    public void Should_Return_Error_When_LastName_Too_Long()
    {
        var result = _validator.ValidateLastName(new string('X', DataConstraints.MaxLastNameLength + 1));
        result.ShouldBeLeft().Which.Message.Should().Contain("Last name must be less than");
    }

    [Test]
    public void Should_Return_Unit_When_LastName_Is_Valid()
    {
        var result = _validator.ValidateLastName("Smith");
        result.ShouldBeRight().Which.Should().Be(Unit.Default);
    }

    [Test]
    public void Should_Return_Error_When_Email_Too_Long()
    {
        var longEmail = new string('a', DataConstraints.MaxEmailLength + 1) + "@test.com";
        var result = _validator.ValidateEmail(longEmail);
        result.ShouldBeLeft().Which.Message.Should().Contain("Email must be less than");
    }

    [Test]
    public void Should_Return_Error_When_Email_Invalid_Format()
    {
        var result = _validator.ValidateEmail("not-an-email");
        result.ShouldBeLeft().Which.Message.Should().Be("Email is not valid");
    }

    [Test]
    public void Should_Return_Unit_When_Email_Is_Valid()
    {
        var result = _validator.ValidateEmail("john.doe@example.com");
        result.ShouldBeRight().Which.Should().Be(Unit.Default);
    }

    [Test]
    public void Should_Return_Unit_When_Email_Is_Null()
    {
        var result = _validator.ValidateEmail(null);
        result.ShouldBeRight().Which.Should().Be(Unit.Default);
    }
}