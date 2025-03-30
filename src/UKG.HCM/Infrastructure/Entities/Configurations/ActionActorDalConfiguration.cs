using CommonContracts.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UKG.HCM.Infrastructure.Entities.Configurations;

internal class ActionActorDalConfiguration : IEntityTypeConfiguration<ActionActorDal>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ActionActorDal> builder)
    {
        builder.ToTable("ActionActors");

        builder.HasKey(x => x.ActorId);
        builder.Property(x => x.ActorId).ValueGeneratedOnAdd();

        builder.Property(x => x.PersonId)
            .IsRequired(false);

        builder.Property(x => x.Service)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.HasOne(a => a.Person)
            .WithMany()
            .HasForeignKey(a => a.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new ActionActorDal(DefaultNames.AnonymousActorName)
        {
            ActorId = DefaultNames.AnonymousActorGuid,
        });
    }
}