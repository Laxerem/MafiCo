using MafiCo.Application.Game.Mediator.Access;
using MafiCo.Application.Game.Mediator.Internal.Commands;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Application.Interfaces.Notifications;
using MediatR;

namespace MafiCo.Application.Game;
using GameEntity = MafiCo.Domain.AggregatesModel.GameAggregate.Game;

public class GameSession : INotifyConsumer, INotifySource {
    public GameEntity Game { get; private set; }
    public event Func<IGameNotification, Task> OnNotification;
    
    private bool _isFinished;
    private Dictionary<Guid, PlayerProcessor> _processors;

    public GameSession(GameEntity game) {
        Game = game;
        _processors = new Dictionary<Guid, PlayerProcessor>();
        _isFinished = false;
    }

    public async Task StartAsync(int mafiaCount, IMediator mediator) {
        if (_isFinished) throw new InvalidOperationException("Game session is finished");
        
        foreach (var playerId in Game.GetAllPlayers()) {
            var processor = await mediator.Send(new CreatePlayerCommand(playerId));
            _processors.Add(playerId, processor);
            processor.Run();
        }
        
        var worker = new GameWorker(Game, mafiaCount, new GamePublisher(mediator));
        await worker.RunAsync();
    }

    public IEnumerable<IProcessor<IGameNotification>> GetProcessors() {
        return _processors.Values;
    }

    public IPlayerSession GetPlayerSession(Guid playerId) {
        return _processors[playerId].Session;
    }

    public Task HandleAsync(IGameNotification domainEvent) {
        // if (Game.FinishedAt is not null) throw new InvalidOperationException("You cannot send the messages when game is finished");
        OnNotification?.Invoke(domainEvent);
        return Task.CompletedTask;
    }

    public void Finish() {
        if (_isFinished) return;
        _isFinished = true;
        _processors.Clear();
    }
}