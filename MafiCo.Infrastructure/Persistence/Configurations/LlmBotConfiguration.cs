using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Configurations;

public class LlmBotConfiguration : EntityConfiguration<LlmBot> {
    public override void Configure(EntityTypeBuilder<LlmBot> builder) {
        base.Configure(builder);
        
        builder.ToTable("LlmBots");
        builder.Property(x => x.ModelName).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.ApiKey).IsRequired();
    }
}