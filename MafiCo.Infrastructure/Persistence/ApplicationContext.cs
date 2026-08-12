using MafiCo.Domain.AggregatesModel.LlmBotAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace MafiCo.Infrastructure;

public class ApplicationContext : DbContext {
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<LlmBot> LlmBots { get; set; }
    private IDbContextTransaction? _currentTransaction = null;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        
    }
    
    
    public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default) {
        if (_currentTransaction != null) return null;
        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction) {
        if (transaction == null) throw new ArgumentNullException("The transaction has not been started");

        try {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch {
            RollbackTransaction();
            throw;
        }
        
        await Database.CommitTransactionAsync();
    }

    public void RollbackTransaction() {
        try {
            _currentTransaction?.Rollback();
        }
        finally {
            if (_currentTransaction != null) {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("mafico");
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new LlmBotConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite($"Data Source=../../../mafico.db");
    }
}