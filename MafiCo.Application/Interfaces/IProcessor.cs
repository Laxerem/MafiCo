using MediatR;

namespace MafiCo.Application.Interfaces;

public interface IProcessor<T> where T : INotification {
    public Guid Id { get; }
    void Run();
    Task SendNotify(T notification);
    void Stop();
}