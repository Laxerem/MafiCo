using MafiCo.Application.Game.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Application.Interfaces.Mediator.Access;

namespace MafiCo.Application.Game.Controllers;

public class DefaultController : IPlayerController {
    private readonly Guid _playerId;
    private readonly IPlayerSender  _sender;

    public DefaultController(Guid playerId, IPlayerSender sender) {
        _playerId = playerId;
        _sender = sender;
    }

    public async Task SendMessage(string message) {
        await _sender.Send(new SendMessageCommand(_playerId, message));
    }
}