using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game;

public class PlayerProcessor : IProcessor<IGameNotification> {
    private readonly PlayerSession _playerSession;
    private readonly INotifySource _notifySource;
    private bool _isRunning;
    
    public Guid Id { get; }
    public IPlayerSession Session => _playerSession;

    public PlayerProcessor(Guid id, INotifySource notifyNotifySource) {
        Id = id;
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