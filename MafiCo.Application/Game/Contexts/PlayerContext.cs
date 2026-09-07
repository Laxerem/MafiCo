using System.Threading.Channels;
using MafiCo.Application.Interfaces.Controllers;
using MediatR;
namespace MafiCo.Application.Game.Contexts;

public class PlayerContext {
    public List<INotification> Events { get; private set; }
    public IPlayerController? Controller { get; private set; }
    public readonly ChannelReader<INotification> EventsReader;
    public event Action OnControllerChanged; 
    private Channel<INotification> _channel;
    

    public PlayerContext() {
        Events = new List<INotification>();
        _channel = Channel.CreateUnbounded<INotification>();
        EventsReader = _channel.Reader;
    }

    public async Task AddNotification(INotification notification) {
        Events.Add(notification);
        await _channel.Writer.WriteAsync(notification);
    }
    
    public void ChangeController(IPlayerController? controller) {
        Controller = controller;
        OnControllerChanged?.Invoke();
    }
}