using MafiCo.Application.Interfaces.Commands;
using MafiCo.Application.Interfaces.Game;

namespace MafiCo.Application.Game.Mediator.Commands;

public record StartGameCommand(int MafiaCount) : IUserCommand<IPlayerSession> {}