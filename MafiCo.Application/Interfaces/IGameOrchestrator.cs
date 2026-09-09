using MafiCo.Application.Game.Contexts;

namespace MafiCo.Application.Interfaces;

public interface IGameOrchestrator {
    Task Initialize(HashSet<Guid> playerIds);
    PlayerContext GetPlayerContext(Guid playerId);
    Task StartAsync(int mafiaCount);
    void StopAll();
}