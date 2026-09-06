using MafiCo.Application.Game;
using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Infrastructure.MediatR.System;

public record CreateProcessorCommand(Guid profileId) : ISystemCommand<PlayerProcessor> {}