using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.People.Update;

/// <inheritdoc />
public class PersonUpdaterValidationDecorator : IUpdatePerson
{
    private readonly IUpdatePerson _service;
    private readonly IPersonDataValidator _validator;

    public PersonUpdaterValidationDecorator(
        IUpdatePerson service,
        IPersonDataValidator validator)
    {
        _service = service;
        _validator = validator;
    }

    /// <inheritdoc />
    public Task<Either<ErrorResult, Unit>> Update(
        PersonUpdateCommand command,
        CancellationToken ct)
    {
        return _validator.ValidateFirstName(command.FirstName)
            .Bind(_ => _validator.ValidateLastName(command.LastName))
            .Bind(_ => _validator.ValidateEmail(command.Email))
            .BindAsync(_ => _service.Update(command, ct));
    }
}