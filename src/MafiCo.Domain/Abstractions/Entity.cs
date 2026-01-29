using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Abstractions;

public abstract class Entity {
    public List<INotification> Notifications { get; protected set; } = new List<INotification>();

    public void AddNotification(INotification notification) {
        Notifications.Add(notification);
    }
}