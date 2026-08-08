using MafiCo.Domain.Entities;
using MafiCo.Domain.Interfaces;
using MediatR;

namespace MafiCo.Domain.Events.Players;

public record PlayerVotedEvent(Guid Id, Guid TargetId) : IDomainEvent;