using MediatR;

namespace MafiCo.Application.Game;

public class PlayerContext {
    public List<INotification> Events { get; private set; }

    public PlayerContext() {
        Events = new List<INotification>();
    }

    public void AddNotification(INotification notification) {
        Events.Add(notification);
    }
}