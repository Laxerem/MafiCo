using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Configurations;

public class LlmBotConfiguration : IEntityTypeConfiguration<LlmBot> {
    public void Configure(EntityTypeBuilder<LlmBot> builder) {
        builder.ToTable("LlmBots");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ModelName).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.ApiKey).IsRequired();
    }
}