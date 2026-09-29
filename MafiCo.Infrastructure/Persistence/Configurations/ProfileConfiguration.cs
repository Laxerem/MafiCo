using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class ProfileConfiguration : AggregateConfiguration<Profile, ProfileId> {
    public override void Configure(EntityTypeBuilder<Profile> builder) {
        base.Configure(builder);

        builder.ToTable("Profiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(id => id.Id, value => new ProfileId(value))
            .ValueGeneratedNever();
        builder
            .Property(x => x.Name)
            .HasMaxLength(15)
            .IsRequired();
        builder.Property(x => x.VictoriesCount).HasDefaultValue(0);
        builder.Property(x => x.DefeatsCount).HasDefaultValue(0);
    }
}
