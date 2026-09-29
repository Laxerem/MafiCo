using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game.DTOs;

public record ResultPlayerInfo(ProfileId Id, string Name, Role Role, bool IsAlive);
