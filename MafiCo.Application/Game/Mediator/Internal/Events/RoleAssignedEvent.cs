using MafiCo.Application.Interfaces.Mediator;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Game.Mediator.Internal.Events;

public record RoleAssignedEvent(IEnumerable<PlayerInfo> AssignedPlayers) : IGameEvent;