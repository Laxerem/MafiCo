using System.Threading.Channels;
using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Controllers;
using MafiCo.Application.Interfaces.Notifications;
using MafiCo.Application.Notifications;
using MediatR;
namespace MafiCo.Application.Game.Contexts;

public class PlayerContext {
    public List<IGameNotification> Events { get; private set; }
    public IPlayerController? Controller { get; private set; }
    public readonly ChannelReader<IGameNotification> EventsReader;
    public event Action OnControllerChanged; 
    private Channel<IGameNotification> _channel;
    

    public PlayerContext() {
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