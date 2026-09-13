using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Game.Mediator.Commands;

public record GetPlayersCommand() : IPlayerCommand<List<PublicPlayerInfo>>;