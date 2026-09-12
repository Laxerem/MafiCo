using MafiCo.Application.Game.Mediator.Internal.Events;
using MafiCo.Application.Interfaces.Mediator.Access;
using GameAggregate = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

namespace MafiCo.Application.Game;

public class GameWorker {
    private readonly IGamePublisher _publisher;
    private readonly GameAggregate _game;
    
    public GameWorker(GameAggregate game, IGamePublisher publisher) {
        _publisher = publisher;
        _game = game;
    }

    public async Task RunAsync(int mafiaCount) {
        await Task.Run(async () => Work(mafiaCount));
    }

    private async Task Work(int mafiaCount) {
        _game.AssignRoles(mafiaCount);
        await _publisher.Publish(new RoleAssignedEvent());
        await _publisher.Publish(new PhaseChangedEvent(_game.Phase));
        await Task.Delay(TimeSpan.FromSeconds(10));

        while (true) {
            _game.NextPhase();
            await _publisher.Publish(new PhaseChangedEvent(_game.Phase));
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}