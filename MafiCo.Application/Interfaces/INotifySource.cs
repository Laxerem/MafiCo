using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Interfaces;

public interface INotifySource {
    public event Func<IGameNotification, Task> OnNotification;
}