using MafiCo.Domain.SeedWork;

namespace MafiCo.Domain.Interfaces;

public interface IAggregateRoot {
    List<IDomainEvent> Notifications { get; }
    void ClearNotifications();
}
