using MediatR;

namespace MafiCo.Application.Interfaces;

public interface IEventConsumer {
    public Task SendEvent(INotification notification);
}