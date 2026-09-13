using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Game.Commands;

public record SendMessageCommand(Guid PlayerId, string Message) : IPlayerCommand {}