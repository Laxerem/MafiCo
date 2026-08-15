using MafiCo.Domain.AggregatesModel.GameAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class GameConfiguration : EntityConfiguration<Game> {
    public override void Configure(EntityTypeBuilder<Game> builder) {
        base.Configure(builder);
        builder.ToTable("Games");
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.FinishedAt).HasDefaultValue(null);
    }
}