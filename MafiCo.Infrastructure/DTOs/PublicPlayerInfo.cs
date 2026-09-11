using MafiCo.Domain.AggregatesModel.GameAggregate;

namespace MafiCo.Infrastructure.DTOs;

public record PublicPlayerInfo(Guid Id, string Name, bool IsAlive);