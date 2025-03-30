using Microsoft.EntityFrameworkCore;
using UKG.HCM.Infrastructure.Entities;

namespace UKG.HCM.Infrastructure.Contexts;

public class PeopleDbContext : DbContext
{
    public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
        : base(options)
    {
    }

    public required DbSet<PersonDal> People { get; init; }
    public required DbSet<AccountDal> Accounts { get; init; }
    public required DbSet<ActionActorDal> ActionActors { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PeopleDbContext).Assembly);
    }
}