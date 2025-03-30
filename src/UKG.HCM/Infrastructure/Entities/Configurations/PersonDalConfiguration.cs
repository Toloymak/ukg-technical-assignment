using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities.Configurations;

/// <summary>
/// Person history EF configuration
/// </summary>
internal sealed class PersonDalConfiguration : IEntityTypeConfiguration<PersonDal>
{
    /// <summary>
    /// Configure EF mapping
    /// </summary>
    public void Configure(EntityTypeBuilder<PersonDal> builder)
    {
        builder.ToTable("People");

        builder.HasKey(x => x.PersonId);

        builder.Property(x => x.PersonId).ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasIndex(a => a.Email)
            .IsUnique();
    }
}