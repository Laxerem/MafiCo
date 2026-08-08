using MafiCo.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MafiCo.Infrastructure;

public class UnitOfWork : IUnitOfWork {
    private readonly ApplicationContext _context;
    private readonly IMediator _mediator;
    
    public UnitOfWork(ApplicationContext context, IMediator mediator) {
        _context = context;
        _mediator = mediator;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
        Console.WriteLine(_context.Database.GetDbConnection().ConnectionString);
        
        await _mediator.DispatchDomainEventsAsync(_context);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}