using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game;

public class PlayerProcessor : IProcessor<IGameNotification> {
    private readonly PlayerSession _playerSession;
    private readonly INotifySource _notifySource;
    private bool _isRunning;

    public Guid Id { get; }
    public IPlayerSession Session => _playerSession;

    public PlayerProcessor(PlayerId id, INotifySource notifyNotifySource) {
        Id = id.Id;
        _playerSession = new PlayerSession();
        _notifySource = notifyNotifySource;
    }

    public void Run() {
        if (_isRunning) return;
        _isRunning = true;
        _notifySource.OnNotification += SendNotify;
    }

    public async Task SendNotify(IGameNotification notification) {
        if (!_isRunning) return;
        await _playerSession.HandleAsync(notification);
    }

    public void Stop() {
        if (!_isRunning) return;
        _isRunning = false;
        _notifySource.OnNotification -= SendNotify;
    }
}