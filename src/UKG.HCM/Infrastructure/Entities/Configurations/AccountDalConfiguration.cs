using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UKG.HCM.Infrastructure.Entities.Configurations;

internal class AccountDalConfiguration : IEntityTypeConfiguration<AccountDal>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AccountDal> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.AccountId);
        builder.Property(x => x.AccountId).ValueGeneratedOnAdd();

        builder.Property(x => x.Login)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(a => a.Login)
            .IsUnique();
        
        builder.Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(a => a.PasswordSalt)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasOne(a => a.Person)
            .WithOne(p => p.Account)
            .HasForeignKey<AccountDal>(a => a.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}