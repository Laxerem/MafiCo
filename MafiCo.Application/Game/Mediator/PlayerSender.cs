using MafiCo.Application.Interfaces.Commands;
using MafiCo.Application.Interfaces.Mediator.Access;
using MediatR;

namespace MafiCo.Application.Game.Mediator;

public class PlayerSender : IPlayerSender {
    private readonly ISender _mediator;
    
    public PlayerSender(ISender mediator) {
        _mediator = mediator;
    }

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default(CancellationToken)) where TRequest : IPlayerCommand {
        return _mediator.Send(request, cancellationToken);
    }
}