using MafiCo.Application.Game;
using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Game.Commands;

public record StartGameCommand(int MafiaCount) : IUserCommand<PlayerView> {}