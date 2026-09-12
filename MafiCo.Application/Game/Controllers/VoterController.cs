using MafiCo.Application.Game.Commands;
using MafiCo.Application.Interfaces;

namespace MafiCo.Application.Game.Controllers;

public class VoterController : IPlayerController {
    private readonly Guid _playerId;
    private readonly IPlayerMediator _mediator;

    public VoterController(Guid playerId, IPlayerMediator mediator) {
        _playerId = playerId;
        _mediator = mediator;
    }

    public async Task MakeVote(Guid targetId) {
        await _mediator.Send(new MakeVoteCommand(_playerId, targetId));
    }
}