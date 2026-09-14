using MafiCo.Application.Interfaces.Commands;
using MafiCo.Application.Interfaces.Game;

namespace MafiCo.Application.Game.Mediator.Internal.Commands;

internal record CreatePlayerCommand(Guid ProfileId) : ISystemCommand<PlayerProcessor> {}