using MafiCo.Application.Game.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Application.Interfaces.Mediator.Access;

namespace MafiCo.Application.Game.Controllers;

public class VoterController : IPlayerController {
    private readonly Guid _playerId;
    private readonly IPlayerSender _sender;

    public VoterController(Guid playerId, IPlayerSender sender) {
        _playerId = playerId;
        _sender = sender;
    }

    public async Task MakeVote(Guid targetId) {
        await _sender.Send(new MakeVoteCommand(_playerId, targetId));
    }
}