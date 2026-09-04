using MafiCo.Application.Abstractions;
using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Infrastructure.MediatR.Game.Commands;

public record StartGameCommand(int MafiaCount) : IAppCommand<PlayerInterface> {}