using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.SeedWork;

public abstract class Entity {
    public readonly Guid Id;
    public List<IDomainEvent> Notifications { get; protected set; }
    public Entity(Guid id) {
        Id = id;
        Notifications = new List<IDomainEvent>();
    }
    
    protected void AddNotification(IDomainEvent notification) {
        Notifications.Add(notification);
    }

    public void ClearNotifications() {
        Notifications.Clear();
    }
}