using MafiCo.Application.Interfaces.Controllers;
using MediatR;
namespace MafiCo.Application.Game.Contexts;

public class PlayerContext {
    public List<INotification> Events { get; private set; }
    public IPlayerController? Controller { get; private set; }
    public event Action OnControllerChanged; 
    public event Action<INotification> OnNotification; 

    public PlayerContext() {
        Events = new List<INotification>();
    }

    public void AddNotification(INotification notification) {
        Events.Add(notification);
        OnNotification?.Invoke(notification);
    }
    
    public void ChangeController(IPlayerController? controller) {
        Controller = controller;
        OnControllerChanged?.Invoke();
    }
}