using MafiCo.Domain.AggregatesModel.GameAggregate.Interfaces.Controllers;
using MafiCo.Domain.Entities.Players;

namespace MafiCo.Infrastructure.Services.Processors;

public class MafiaProcessor(Guid playerId, Role role, IPlayerController controller) : PlayerProcessor(playerId, role, controller) {}