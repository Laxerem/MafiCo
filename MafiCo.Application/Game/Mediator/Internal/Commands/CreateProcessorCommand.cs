using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Game.Mediator.Internal.Commands;

internal record CreateProcessorCommand(Guid profileId) : ISystemCommand<PlayerProcessor> {}