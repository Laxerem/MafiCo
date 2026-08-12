using MafiCo.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MafiCo.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork {
    private readonly ApplicationContext _context;
    private readonly IMediator _mediator;
    
    public UnitOfWork(ApplicationContext context, IMediator mediator) {
        _context = context;
        _mediator = mediator;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
        await _mediator.DispatchDomainEventsAsync(_context);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default) {
        return await _context.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction) {
        await _context.CommitTransactionAsync(transaction);
    }

    public void RollbackTransaction() {
        _context.RollbackTransaction();
    }
}