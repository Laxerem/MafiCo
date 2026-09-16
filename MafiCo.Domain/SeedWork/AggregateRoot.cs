namespace MafiCo.Domain.SeedWork;

public abstract class AggregateRoot : Entity {
    public List<IDomainEvent> Notifications { get; protected set; }
    public AggregateRoot(Guid id) :  base(id) {
        Notifications = new List<IDomainEvent>();
    }
    
    protected void AddNotification(IDomainEvent notification) {
        Notifications.Add(notification);
    }

    public void ClearNotifications() {
        Notifications.Clear();
    }
}