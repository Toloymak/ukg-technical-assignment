using CommonContracts.Constants;
using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.People;

public interface IPersonDataValidator
{
    /// Validate the first name
    Either<ErrorResult, Unit> ValidateFirstName(string firstName);
    
    /// Validate the last name
    Either<ErrorResult, Unit> ValidateLastName(string lastName);
    
    /// Validate the email
    Either<ErrorResult, Unit> ValidateEmail(string? email);
}

/// <inheritdoc />
internal class PersonDataValidator : IPersonDataValidator
{
    /// <inheritdoc />
    public Either<ErrorResult, Unit> ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return new ErrorResult("First name is required");
        
        if (firstName.Length > DataConstraints.MaxFirstNameLength)
            return new ErrorResult($"First name must be less than {DataConstraints.MaxFirstNameLength} characters");
        
        return Unit.Default;
    }

    /// <inheritdoc />
    public Either<ErrorResult, Unit> ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return new ErrorResult("Last name is required");
        
        if (lastName.Length > DataConstraints.MaxLastNameLength)
            return new ErrorResult($"Last name must be less than {DataConstraints.MaxLastNameLength} characters");
        
        return Unit.Default;
    }

    /// <inheritdoc />
    public Either<ErrorResult, Unit> ValidateEmail(string? email)
    {
        if (email != null && email.Length > DataConstraints.MaxEmailLength)
            return new ErrorResult($"Email must be less than {DataConstraints.MaxEmailLength} characters");
        
        if (email != null && !ValidationPatterns.EmailRegex().IsMatch(email))
            return new ErrorResult("Email is not valid");

        return Unit.Default;
    }
}