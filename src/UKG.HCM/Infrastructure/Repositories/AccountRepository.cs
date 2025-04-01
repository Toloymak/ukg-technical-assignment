using System.Linq.Expressions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Entities.BaseTypes;
using UKG.HCM.Application.Errors;
using UKG.HCM.Application.Persistence;
using UKG.HCM.Infrastructure.Contexts;
using UKG.HCM.Infrastructure.Entities;

namespace UKG.HCM.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task<Account?> GetAccountByLogin(string login, CancellationToken ct)
    {
        return _context.Accounts
            .Where(x => x.Login == login) // Todo: make case insensitive
            .Select(MapAction())
            .FirstOrDefaultAsync(ct);
    }

    /// <inheritdoc />
    public Task<Account?> GetAccount(Guid accountId, CancellationToken ct)
    {
        
        return _context.Accounts
            .Where(x => x.AccountId == accountId)
            .Select(MapAction())
            .FirstOrDefaultAsync(ct);
    }

    /// <inheritdoc />
    public async Task<Either<ErrorResult, Unit>> Update(
        Account account,
        CancellationToken ct)
    {
        var dal = await _context.Accounts
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.AccountId == account.AccountId, ct);

        if (dal == null)
            return new NotFoundError("Account not found");

        var accountRoles = await _context.Roles
            .Where(x => account.Roles.Contains(x.Name))
            .ToListAsync(ct);
        
        dal.Roles = accountRoles;
        dal.Login = account.Login;
        dal.PasswordHash = account.PasswordHash;
        dal.PasswordSalt = account.PasswordSalt;

        await _context.SaveChangesAsync(ct);
        return Unit.Default;
    }

    // TODO: Use projection instead
    private static Expression<Func<AccountDal, Account>> MapAction()
    {
        return x => new Account
        {
            AccountId = x.AccountId,
            Login = x.Login,
            PasswordHash = x.PasswordHash,
            Person = new Person()
            {
                Email = new Email(x.Person!.Email),
                Name = new Name
                {
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName
                },
                PersonId = x.Person.PersonId
            },
            PasswordSalt = x.PasswordSalt,
            Roles = x.Roles.Select(role => role.Name).ToHashSet()
        };
    }
}