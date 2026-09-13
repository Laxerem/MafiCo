using System.Threading.Channels;
using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game;

public class PlayerView {
    public List<IGameNotification> Events { get; private set; }
    public IPlayerController? Controller { get; private set; }
    public readonly ChannelReader<IGameNotification> EventsReader;
    private Channel<IGameNotification> _channel;
    

    public PlayerView() {
        Events = new List<IGameNotification>();
        _channel = Channel.CreateUnbounded<IGameNotification>();
        EventsReader = _channel.Reader;
    }

    internal async Task AddNotificationAsync(IGameNotification notification) {
        Events.Add(notification);
        await _channel.Writer.WriteAsync(notification);
    }
    
    internal void ChangeController(IPlayerController? controller) {
        Controller = controller;
        _channel.Writer.TryWrite(new ControllerChangedNotification(controller));
    }
}