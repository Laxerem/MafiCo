using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile> {
    public void Configure(EntityTypeBuilder<Profile> builder) {
        builder.ToTable("Profiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder
            .Property(x => x.Name)
            .HasMaxLength(15)
            .IsRequired();
        builder.Property(x => x.VictoriesCount).HasDefaultValue(0);
        builder.Property(x => x.DefeatsCount).HasDefaultValue(0);
    }
}