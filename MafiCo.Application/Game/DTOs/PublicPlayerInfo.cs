using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Application.Game.DTOs;

public record PublicPlayerInfo(Guid Id, string Name, bool IsAlive);