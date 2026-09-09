using MafiCo.Application.Interfaces.Controllers;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Notifications;

public record ControllerChangedNotification(IPlayerController? Controller) : ISystemNotification {}