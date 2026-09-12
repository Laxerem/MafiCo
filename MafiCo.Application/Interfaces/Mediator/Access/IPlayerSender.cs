using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Interfaces.Mediator.Access;

public interface IPlayerSender {
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default (CancellationToken)) where TRequest : IPlayerCommand;
}