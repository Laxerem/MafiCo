using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Domain.DTOs;

public record PlayerInfo(
    Guid Id,
    Role Role
);
