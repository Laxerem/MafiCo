using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Application.Interfaces.Mediator.Access;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game.Controllers;

public class DefaultController : IPlayerController {
    private readonly PlayerId _playerId;
    private readonly IPlayerSender  _sender;

    public DefaultController(PlayerId playerId, IPlayerSender sender) {
        _playerId = playerId;
        _sender = sender;
    }

    public async Task SendMessage(string message) {
        await _sender.Send(new SendMessageCommand(_playerId, message));
    }
}