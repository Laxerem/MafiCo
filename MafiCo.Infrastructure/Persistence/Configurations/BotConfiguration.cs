using MafiCo.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class BotConfiguration : IEntityTypeConfiguration<BotEntity> {
    public void Configure(EntityTypeBuilder<BotEntity> builder) {
        builder.ToTable("Bots");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ProfileId).IsRequired();
        builder.Property(x => x.LlmId).IsRequired();

        builder
            .HasOne(x => x.Profile)
            .WithMany()
            .HasForeignKey(x => x.ProfileId)
            .IsRequired();

        builder
            .HasOne(x => x.LlmEntity)
            .WithMany()
            .HasForeignKey(x => x.LlmId)
            .IsRequired();
    }
}
