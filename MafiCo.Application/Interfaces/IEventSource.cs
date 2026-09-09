using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate;
using MediatR;

namespace MafiCo.Application.Interfaces;

public interface IEventSource {
    public event Func<IGameNotification, Task> OnNotification;
}