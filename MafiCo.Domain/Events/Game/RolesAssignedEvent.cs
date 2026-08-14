using System.ComponentModel;
using MafiCo.Domain.Entities.Players;
using MafiCo.Domain.Interfaces;

namespace MafiCo.Domain.Events.Game;

[ReadOnly(true)]
public record AssignedData(Guid Id, Role Role);

[ReadOnly(true)]
public record RolesAssignedEvent(List<AssignedData> Events) : IDomainEvent;