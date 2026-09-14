using System.Threading.Channels;
using MafiCo.Application.Interfaces.Notifications;

namespace MafiCo.Application.Interfaces.Game;

public interface IPlayerSession {
    IPlayerController? Controller { get; }
    ChannelReader<IGameNotification> EventsReader { get; }
}