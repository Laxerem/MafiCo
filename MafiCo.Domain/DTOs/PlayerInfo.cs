using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.Entities.Players;

namespace MafiCo.Domain.DTOs;

public record PlayerInfo(
    Guid Id,
    Role Role
);