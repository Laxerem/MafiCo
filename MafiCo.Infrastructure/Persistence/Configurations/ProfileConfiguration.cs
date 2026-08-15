using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Configurations;

public class ProfileConfiguration : EntityConfiguration<Profile> {
    public override void Configure(EntityTypeBuilder<Profile> builder) {
        base.Configure(builder);
        
        builder.ToTable("Profiles");
        builder
            .Property(x => x.Name)
            .HasMaxLength(15)
            .IsRequired();
        builder.Property(x => x.VictoriesCount).HasDefaultValue(0);
        builder.Property(x => x.DefeatsCount).HasDefaultValue(0);
    }
}