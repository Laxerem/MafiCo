using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public class GameConfiguration : AggregateConfiguration<Game, GameId> {
    public override void Configure(EntityTypeBuilder<Game> builder) {
        base.Configure(builder);
        builder.ToTable("Games");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(id => id.Id, value => new GameId(value))
            .ValueGeneratedNever();
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.FinishedAt).HasDefaultValue(null);
        builder.Ignore(x => x.Phase);
    }
}
