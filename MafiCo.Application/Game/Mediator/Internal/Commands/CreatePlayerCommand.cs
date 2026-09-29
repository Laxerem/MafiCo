using MafiCo.Application.Interfaces.Commands;
using MafiCo.Application.Interfaces.Game;
using MafiCo.Domain.AggregatesModel.GameAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Game.Mediator.Internal.Commands;

internal record CreatePlayerCommand(PlayerId PlayerId) : ISystemCommand<PlayerProcessor> {}
