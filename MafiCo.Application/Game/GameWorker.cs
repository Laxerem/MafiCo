using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Interfaces.Mediator.Access;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game;

public class GameWorker {
    private readonly IGamePublisher _publisher;
    private readonly GameAggregate _game;
    private int _mafiaCount;
    
    public GameWorker(GameAggregate game, int mafiaCount, IGamePublisher publisher) {
        _publisher = publisher;
        _mafiaCount = mafiaCount;
        _game = game;
    }

    public async Task RunAsync() {
        await Task.Run(async () => Work(_mafiaCount));
    }

    private async Task Work(int mafiaCount) {
        _game.AssignRoles(mafiaCount);
        await _publisher.Publish(new RoleAssignedEvent());
        await _publisher.Publish(new PhaseChangedEvent(_game.Phase));

        while (true) {
            _game.NextPhase();
            await _publisher.Publish(new PhaseChangedEvent(_game.Phase));
        }
    }
}