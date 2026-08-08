using MafiCo.Domain.SeedWork;
using MediatR;

namespace MafiCo.Infrastructure;

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
}