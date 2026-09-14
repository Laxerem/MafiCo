using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Interfaces;

public interface INotifyConsumer { 
    Task HandleAsync(IGameNotification domainEvent);
}