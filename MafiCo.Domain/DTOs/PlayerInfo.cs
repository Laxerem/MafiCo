using MafiCo.Domain.AggregatesModel.GameAggregate;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Domain.DTOs;

public record PlayerInfo(
    PlayerId Id,
    Role Role,
    bool IsAlive
);
