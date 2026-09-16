using MafiCo.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MafiCo.Infrastructure.Persistence.Configurations;

public abstract class AggregateConfiguration<T> : IEntityTypeConfiguration<T> where T : AggregateRoot {
    
    public virtual void Configure(EntityTypeBuilder<T> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Ignore(x => x.Notifications);
    }
}