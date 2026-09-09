using MafiCo.Domain.AggregatesModel.LlmAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class LlmConfiguration : EntityConfiguration<Llm> {
    public override void Configure(EntityTypeBuilder<Llm> builder) {
        base.Configure(builder);
        builder.ToTable("LlmData");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ModelName).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.ApiKey).IsRequired();
    }
}