using MafiCo.Domain.SeedWork;
using MediatR;

namespace MafiCo.Infrastructure;

public static class MediatorExtension {
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationContext context) {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications.Any());
        
        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();
        
        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearNotifications());

        foreach (var domainEvent in domainEvents) {
            await mediator.Publish(domainEvent);
        }
    }
}