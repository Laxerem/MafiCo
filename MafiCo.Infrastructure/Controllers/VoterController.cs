using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Controllers;
using MafiCo.Infrastructure.MediatR.Game.Commands;

namespace MafiCo.Infrastructure.Controllers;

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