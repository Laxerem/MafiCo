using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.DTOs;
using MafiCo.Infrastructure.DTOs;

namespace MafiCo.Infrastructure.MediatR.Game.Commands;

public record GetPlayersCommand() : IPlayerCommand<List<PublicPlayerInfo>>;