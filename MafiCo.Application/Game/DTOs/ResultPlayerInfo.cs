using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Game.DTOs;

public record ResultPlayerInfo(Guid Id, string Name, Role Role, bool IsAlive);