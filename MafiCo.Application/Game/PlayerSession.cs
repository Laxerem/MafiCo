using System.Threading.Channels;
using MafiCo.Application.Game.Notifications;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game;

public class PlayerSession : IPlayerSession, INotifyConsumer {
    private Channel<IGameNotification> _channel;
    
    public IPlayerController? Controller { get; private set; }
    public ChannelReader<IGameNotification> EventsReader { get; private set; }
    

    public PlayerSession() {
        _channel = Channel.CreateUnbounded<IGameNotification>();
        EventsReader = _channel.Reader;
    }

    public async Task HandleAsync(IGameNotification notification) {
        switch (notification) {
            case ControllerChangedNotification controllerChangedNotification:
                Controller = controllerChangedNotification.Controller;
                break;
        }
        await _channel.Writer.WriteAsync(notification);
    }
}