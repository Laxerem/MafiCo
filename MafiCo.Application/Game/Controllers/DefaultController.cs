using MafiCo.Application.Game.Commands;
using MafiCo.Application.Interfaces;
using MediatR;

namespace MafiCo.Application.Game.Controllers;

public class DefaultController : IPlayerController {
    private readonly Guid _playerId;
    private readonly IMediator _mediator;

    public DefaultController(Guid playerId, IMediator mediator) {
        _playerId = playerId;
        _mediator = mediator;
    }

    public async Task SendMessage(string message) {
        await _mediator.Send(new SendMessageCommand(_playerId, message));
    }
}