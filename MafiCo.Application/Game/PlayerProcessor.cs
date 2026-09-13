using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;
using MediatR;

namespace MafiCo.Application.Game;

public class PlayerProcessor {
    public readonly PlayerView View;
    protected readonly INotifySource NotifySource;
    private bool _isRunning;

    public PlayerProcessor(INotifySource notifyNotifySource) {
        View = new PlayerView();
        NotifySource = notifyNotifySource;
    }

    public void Run() {
        if (_isRunning) return;
        _isRunning = true;
        NotifySource.OnNotification += SendNotify;
    }

    public async Task SendNotify(IGameNotification notification) {
        if (!_isRunning) return;
        await View.AddNotificationAsync(notification);
    }

    public void SetController(IPlayerController? playerController) {
        if (!_isRunning) return;
        View.ChangeController(playerController);
    }

    public void Stop() {
        if (!_isRunning) return;
        _isRunning = false;
        NotifySource.OnNotification -= SendNotify;
    }
}