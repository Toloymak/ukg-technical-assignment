using Microsoft.EntityFrameworkCore;
using UKG.HCM.Infrastructure.Entities;

namespace UKG.HCM.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<PersonDal> People { get; init; }
    public DbSet<AccountDal> Accounts { get; init; }
    public DbSet<ActionActorDal> ActionActors { get; init; }
    public DbSet<RoleDal> Roles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}