using MafiCo.Application.Game;

namespace MafiCo.Application.Interfaces.Mediator.Access;

public interface IGamePublisher {
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default (CancellationToken)) where TNotification : IGameEvent;
}