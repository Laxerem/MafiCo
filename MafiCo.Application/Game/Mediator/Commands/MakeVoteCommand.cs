using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Game.Mediator.Commands;

public record MakeVoteCommand(Guid PlayerId, Guid TargetId) : IPlayerCommand {}