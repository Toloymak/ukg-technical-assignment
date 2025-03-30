using LanguageExt;
using UKG.HCM.Application.Errors;

namespace UKG.HCM.Application.Services.People.Create;

public class PersonCreatorValidationDecorator : ICreatePerson
{
    private readonly ICreatePerson _service;
    private readonly IPersonDataValidator _validator;

    public PersonCreatorValidationDecorator(
        ICreatePerson service,
        IPersonDataValidator validator)
    {
        _service = service;
        _validator = validator;
    }

    /// <inheritdoc />
    public Task<Either<ErrorResult, Guid>> Create(
        PersonCreateCommand command,
        CancellationToken ct)
    {
        return _validator.ValidateFirstName(command.FirstName)
            .Bind(_ => _validator.ValidateLastName(command.LastName))
            .Bind(_ => _validator.ValidateEmail(command.Email))
            .BindAsync(_ => _service.Create(command, ct));
    }
    
   
}