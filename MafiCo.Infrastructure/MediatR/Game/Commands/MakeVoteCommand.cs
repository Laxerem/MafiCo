using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Infrastructure.MediatR.Game.Commands;

public record MakeVoteCommand(Guid PlayerId, Guid TargetId) : IPlayerCommand {}