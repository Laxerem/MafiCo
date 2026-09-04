using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class BotConfiguration : EntityConfiguration<Bot> {
    public override void Configure(EntityTypeBuilder<Bot> builder) {
        base.Configure(builder);
        builder.ToTable("Bots");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ProfileId).IsRequired();

        builder
            .HasOne<Profile>()
            .WithMany()
            .HasForeignKey(x => x.ProfileId)
            .IsRequired();

        builder
            .HasOne<Llm>()
            .WithMany()
            .HasForeignKey(x => x.LlmId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
