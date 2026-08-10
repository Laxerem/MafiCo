using MafiCo.Domain.DTOs;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events;

public record GameFinishedEvent(IReadOnlyList<PlayerInfo> Winners, IReadOnlyList<PlayerInfo> Losers) : IDomainEvent;