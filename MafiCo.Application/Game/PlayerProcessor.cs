using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Application.Notifications;
using MediatR;

namespace MafiCo.Application.Game;

public class PlayerProcessor {
    public readonly PlayerContext Context;
    protected readonly IEventSource _eventSource;
    private bool _isRunning;

    public PlayerProcessor(IEventSource eventEventSource) {
        Context = new PlayerContext();
        _eventSource = eventEventSource;
    }

    public async Task RunAsync() {
        _isRunning = true;
        _eventSource.OnNotification += SendNotify;
    }

    public async Task SendNotify(INotification notification) {
        if (_isRunning) {
            switch (notification) {
                case IGameNotification playerNotification:
                    await Context.AddNotificationAsync(playerNotification);
                    break;
                case ControllerChangedNotification controllerNotification:
                    Context.ChangeController(controllerNotification.Controller);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(notification), notification, null);
            }
        }
    }

    public void Stop() {
        
    }
}