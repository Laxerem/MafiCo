using MediatR;

namespace MafiCo.Application.Interfaces;

public interface IEventSource {
    public event Action<INotification> OnNotification;
}