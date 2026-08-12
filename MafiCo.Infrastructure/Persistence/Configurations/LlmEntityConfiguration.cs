using MafiCo.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class LlmEntityConfiguration : IEntityTypeConfiguration<LlmEntity> {
    public void Configure(EntityTypeBuilder<LlmEntity> builder) {
        builder.ToTable("LlmData");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ModelName).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.ApiKey).IsRequired();
    }
}