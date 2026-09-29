using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;

namespace MafiCo.Application.Game.Mediator.Commands;

public record SendMessageCommand(PlayerId PlayerId, string Message) : IPlayerCommand {}
