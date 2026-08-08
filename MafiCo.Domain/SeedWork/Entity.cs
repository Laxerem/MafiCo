using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.SeedWork;

public abstract class Entity {
    public readonly Guid Id;
    public List<IDomainEvent> Notifications { get; protected set; }
    public Entity() {
        Id = Guid.NewGuid();
        Notifications = new List<IDomainEvent>();
    }
    
    public void AddNotification(IDomainEvent notification) {
        Notifications.Add(notification);
    }

    public void ClearNotifications() {
        Notifications.Clear();
    }
}