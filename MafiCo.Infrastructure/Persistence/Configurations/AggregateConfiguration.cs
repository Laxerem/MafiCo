using MafiCo.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public abstract class AggregateConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : AggregateRoot<TId> {

    public virtual void Configure(EntityTypeBuilder<TEntity> builder) {
        builder.Ignore(x => x.Notifications);
    }
}
