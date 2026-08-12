using Microsoft.EntityFrameworkCore.Storage;

namespace MafiCo.Infrastructure.Interfaces;

public interface IUnitOfWork {
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    void RollbackTransaction();
}