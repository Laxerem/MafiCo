using MafiCo.Application.Game.DTOs;
using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Game.Commands;

public record GetPlayersCommand() : IPlayerCommand<List<PublicPlayerInfo>>;