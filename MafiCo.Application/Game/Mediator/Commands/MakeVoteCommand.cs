using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game.Mediator.Commands;

public record MakeVoteCommand(PlayerId PlayerId, PlayerId TargetId) : IPlayerCommand {}
