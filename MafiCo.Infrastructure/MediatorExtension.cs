using MafiCo.Domain.Interfaces;
using MafiCo.Infrastructure.Persistence;
using MediatR;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MafiCo.Infrastructure;

public static class MediatorExtension {
    private static readonly ILogger Logger = Log.ForContext(typeof(MediatorExtension));

    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationContext context) {
        var domainEntities = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity.Notifications.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearNotifications());

        foreach (var domainEvent in domainEvents) {
            Logger.Information("Dispatching domain event {EventType}", domainEvent.GetType().Name);
            await mediator.Publish(domainEvent);
        }
    }
}
