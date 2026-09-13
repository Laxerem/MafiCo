using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Game.Mediator.Internal.Commands;

internal record CreateProcessorCommand(Guid ProfileId) : ISystemCommand<PlayerProcessor> {}