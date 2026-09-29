using MafiCo.Application.Interfaces;
using MediatR;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MafiCo.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork {
    private readonly ApplicationContext _context;
    private readonly IMediator _mediator;
    private readonly ILogger _logger = Log.ForContext<UnitOfWork>();

    public UnitOfWork(ApplicationContext context, IMediator mediator) {
        _context = context;
        _mediator = mediator;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
        await _mediator.DispatchDomainEventsAsync(_context);
        _logger.Information("Invoke the save changes method");
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
