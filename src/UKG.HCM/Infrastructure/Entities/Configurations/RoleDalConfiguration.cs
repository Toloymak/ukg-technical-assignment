using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UKG.HCM.Infrastructure.Entities.Configurations;

/// <summary>
/// Person history EF configuration
/// </summary>
internal sealed class RoleDalConfiguration : IEntityTypeConfiguration<RoleDal>
{
    /// <summary>
    /// Configure EF mapping
    /// </summary>
    public void Configure(EntityTypeBuilder<RoleDal> builder)
    {
        builder.ToTable("Roles");
        
        builder.HasKey(x => x.Name);
        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasData(
            new() { Name = "HR ADMIN" },
            new() { Name = "MANAGER" },
            new() { Name = "EMPLOYEE" });
    }
}