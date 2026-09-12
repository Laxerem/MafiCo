using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Interfaces;

public interface INotifyConsumer {
    public Task SendNotify(IGameNotification domainEvent);
}