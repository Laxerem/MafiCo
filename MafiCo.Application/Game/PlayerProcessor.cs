using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces;
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
    }

    public void SendNotify(INotification notification) {
        if (_isRunning) {
            Context.AddNotification(notification);
        }
    }

    public void Stop() {
        
    }
}