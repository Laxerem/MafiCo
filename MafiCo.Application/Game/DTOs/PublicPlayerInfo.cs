using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game.DTOs;

public record PublicPlayerInfo(PlayerId Id, string Name, bool IsAlive);
