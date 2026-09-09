using MafiCo.Application.Interfaces.Controllers;
using MafiCo.Infrastructure.MediatR.Game.Commands;
using MediatR;

namespace MafiCo.Infrastructure.Controllers;

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