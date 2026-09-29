using MafiCo.Application.Game.Mediator.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Application.Interfaces.Mediator.Access;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game.Controllers;

public class VoterController : IPlayerController {
    private readonly PlayerId _playerId;
    private readonly IPlayerSender _sender;

    public VoterController(PlayerId playerId, IPlayerSender sender) {
        _playerId = playerId;
        _sender = sender;
    }

    public async Task MakeVote(PlayerId targetId) {
        await _sender.Send(new MakeVoteCommand(_playerId, targetId));
    }
}