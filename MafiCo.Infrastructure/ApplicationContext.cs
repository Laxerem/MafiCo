using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MafiCo.Infrastructure;

public class ApplicationContext : DbContext {
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<LlmBot> LlmBots { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("mafico");
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
    }
}