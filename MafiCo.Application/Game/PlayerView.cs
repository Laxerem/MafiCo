using System.Threading.Channels;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Game;

public class PlayerView {
    public List<IGameNotification> Events { get; private set; }
    public IPlayerController? Controller { get; private set; }
    public readonly ChannelReader<IGameNotification> EventsReader;
    public event Action OnControllerChanged; 
    private Channel<IGameNotification> _channel;
    

    public PlayerView() {
        Events = new List<IGameNotification>();
        _channel = Channel.CreateUnbounded<IGameNotification>();
        EventsReader = _channel.Reader;
    }

    public async Task AddNotificationAsync(IGameNotification notification) {
        Events.Add(notification);
        await _channel.Writer.WriteAsync(notification);
    }
    
    public void ChangeController(IPlayerController? controller) {
        Controller = controller;
        OnControllerChanged?.Invoke();
    }
}