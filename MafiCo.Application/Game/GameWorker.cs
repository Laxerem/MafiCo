using MafiCo.Application.Game.Mediator.Internal.Events;
using MediatR;
using Serilog;
using ILogger = Serilog.ILogger;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game;

public class GameWorker {
    private readonly IMediator _mediator;
    private readonly GameAggregate _game;
    private int _mafiaCount;
    private readonly ILogger _logger = Log.ForContext<GameWorker>();

    public GameWorker(GameAggregate game, int mafiaCount, IMediator mediator) {
        _game = game;
        _mafiaCount = mafiaCount;
        _mediator = mediator;
    }

    public async Task RunAsync(Action? onFailure = null) {
        try {
            var assignedPlayers = _game.AssignRoles(_mafiaCount);
            await _mediator.Publish(new RoleAssignedEvent(assignedPlayers));

            while (true) {
                await _mediator.DispatchGameEvents(_game);
                
                var activePlayers = _game.NextPhase();
                await _mediator.Publish(new PhaseChangedEvent(_game.Phase, activePlayers));
                await Task.Delay(4000);
                
                var victim = _game.ResolveVoting();
                await _mediator.Publish(new VotingFinishedEvent(victim));
            }
        }
        catch (Exception ex) {
            _logger.Error(ex, "Game worker loop failed");
            onFailure?.Invoke();
        }
    }
}
