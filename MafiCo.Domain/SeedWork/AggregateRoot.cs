using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.SeedWork;

public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot {
    public List<IDomainEvent> Notifications { get; protected set; }
    public AggregateRoot(T id) :  base(id) {
        Notifications = new List<IDomainEvent>();
    }

    protected void AddNotification(IDomainEvent notification) {
        Notifications.Add(notification);
    }

    public void ClearNotifications() {
        Notifications.Clear();
    }
}