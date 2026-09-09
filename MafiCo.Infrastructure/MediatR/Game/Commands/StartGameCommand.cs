using MafiCo.Application.Game.Contexts;
using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Infrastructure.MediatR.Game.Commands;

public record StartGameCommand(int MafiaCount) : IUserCommand<PlayerContext> {}