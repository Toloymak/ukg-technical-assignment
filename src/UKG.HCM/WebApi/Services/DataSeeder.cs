using Microsoft.EntityFrameworkCore;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Services.Accounts;
using UKG.HCM.Application.Services.People.Create;
using UKG.HCM.Infrastructure.Contexts;
using UKG.HCM.Infrastructure.Entities;

namespace UKG.HCM.WebApi.Services;

public interface ISeedData
{
    /// Seed default test data
    Task SeedTestData();
}

public class DataSeeder : ISeedData
{
    private readonly AppDbContext _context;
    private readonly ICreatePerson _personCreator;
    private readonly ILogger<DataSeeder> _logger;
    private readonly ISetPassword _setPassword;

    public DataSeeder(
        AppDbContext context,
        ICreatePerson personCreator,
        ILogger<DataSeeder> logger,
        ISetPassword setPassword)
    {
        _context = context;
        _personCreator = personCreator;
        _logger = logger;
        _setPassword = setPassword;
    }

    private const string DefaultAccountPassword = "Pass1234";

    /// <inheritdoc />
    public async Task SeedTestData()
    {
        await SeedAccount(DefaultAdmin);
        await SeedAccount(DefaultManager);
        await SeedAccount(DefaultEmployee);
    }

    private async Task SeedAccount(Account account)
    {
        var emailString = account.Person.Email?.Value;
        var existsPerson = await _context.People
            .FirstOrDefaultAsync(x => x.Email == emailString);
        
        
        var personId = existsPerson?.PersonId;
        if (existsPerson == null)
            personId = await _personCreator.Create(
                new PersonCreateCommand
                {
                    FirstName = account.Person.Name.FirstName,
                    LastName = account.Person.Name.LastName,
                    Email = emailString
                }, CancellationToken.None)
                .ToAsync()
                .Match(id => id, _ => (Guid?) null);

        if (personId is null)
        {
            _logger.LogError("Error seeding default user: {Email}", emailString);
            return;
        }
        
        var existsAccount = await _context.Accounts
            .FirstOrDefaultAsync(x => x.PersonId == personId);

        if (existsAccount != null)
        {
            _logger.LogInformation("Account already exists: {Email}", emailString);
            return;
        }
        
        var rolesToAdd = await _context.Roles
                 .Where(x => account.Roles.Contains(x.Name))
                 .ToArrayAsync();
            
        var accountDal = new AccountDal()
        {
            PasswordHash = string.Empty,
            Login = account.Login,
            PersonId = personId.Value,
            PasswordSalt = string.Empty,
            Roles = rolesToAdd
        };

        _context.Accounts.Add(accountDal);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Seeded account: {Email}", emailString);
        
        var setPasswordResult = await _setPassword
            .SetPassword(accountDal.AccountId, DefaultAccountPassword, CancellationToken.None);
        
        setPasswordResult.IfLeft(_ => _logger.LogError("Error setting password for account: {Email}", emailString));
        setPasswordResult.IfRight(_ => _logger.LogInformation("Set password for account: {Email}", emailString));
    }


    private static readonly Account DefaultAdmin = new()
    {
        AccountId = new Guid("00000000-0000-0000-0000-000000000011"),
        Login = "admin",
        PasswordHash = string.Empty,
        PasswordSalt = string.Empty,
        Roles = ["HR ADMIN"],
        Person = new Person
        {
            PersonId = new Guid("00000000-0000-0000-0000-000000000001"),
            Email = new Email("default.admin@test.hcm"),
            Name = new Name
            {
                FirstName = "Default",
                LastName = "Admin"
            }
        }
    };

    private static readonly Account DefaultManager = new()
    {
        AccountId = new Guid("00000000-0000-0000-0000-000000000012"),
        Login = "manager",
        PasswordHash = string.Empty,
        PasswordSalt = string.Empty,
        Roles = ["MANAGER"],
        Person = new Person
        {
            PersonId = new Guid("00000000-0000-0000-0000-000000000002"),
            Email = new Email("default.manager@test.hcm"),
            Name = new Name
            {
                FirstName = "Default",
                LastName = "Manager"
            }
        }
    };

    private static readonly Account DefaultEmployee = new()
    {
        AccountId = new Guid("00000000-0000-0000-0000-000000000013"),
        Login = "employee",
        PasswordHash = string.Empty,
        PasswordSalt = string.Empty,
        Roles = ["EMPLOYEE"],
        Person = new Person
        {
            PersonId = new Guid("00000000-0000-0000-0000-000000000003"),
            Email = new Email("default.employee@test.hcm"),
            Name = new Name
            {
                FirstName = "Default",
                LastName = "Employee"
            }
        }
    };
}