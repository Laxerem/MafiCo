using System.ComponentModel;
using MafiCo.Domain.Entities.Players;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events.Game;

public record AssignedData(Guid Id, Role Role);

public record RolesAssignedEvent(List<AssignedData> Events) : IDomainEvent;