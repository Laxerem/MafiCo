using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Domain.Entities.Players;

namespace MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces;

public interface IGameController : ISettingController, IPlayerController {
    Role CheckRole(Guid playerId);
    HashSet<Guid> GetAllPlayers();
    void Finish();
}