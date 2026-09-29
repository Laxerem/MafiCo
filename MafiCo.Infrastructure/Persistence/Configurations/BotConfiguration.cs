using MafiCo.Domain.AggregatesModel.BotAggregate;
using MafiCo.Domain.AggregatesModel.BotAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.LlmAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class BotConfiguration : AggregateConfiguration<Bot, BotId> {
    public override void Configure(EntityTypeBuilder<Bot> builder) {
        base.Configure(builder);
        builder.ToTable("Bots");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(id => id.Id, value => new BotId(value))
            .ValueGeneratedNever();

        builder.Property(x => x.ProfileId)
            .HasConversion(id => id.Id, value => new ProfileId(value))
            .IsRequired();

        builder.Property(x => x.LlmId)
            .HasConversion(
                id => id.HasValue ? id.Value.Id : (Guid?)null,
                value => value.HasValue ? new LlmId(value.Value) : (LlmId?)null);

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
