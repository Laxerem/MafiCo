using MediatR;

namespace MafiCo.Infrastructure.Interfaces;

public interface IDispatcher<T> where T : INotification {
    public void Handle(T notification);
}