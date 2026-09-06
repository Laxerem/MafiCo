using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces;
using MediatR;

namespace MafiCo.Application.Game;

public class PlayerProcessor {
    public readonly PlayerContext Context;
    protected readonly IEventSource _eventSource;

    public PlayerProcessor(IEventSource eventEventSource) {
        Context = new PlayerContext();
        _eventSource = eventEventSource;
    }

    public async Task RunAsync() {}

    public void SendNotify(INotification notification) {
        Context.AddNotification(notification);
    }

    public void Stop() {
        
    }
}