using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Interfaces;

public interface IPlayerMediator {
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default (CancellationToken)) where TRequest : IPlayerCommand;
}