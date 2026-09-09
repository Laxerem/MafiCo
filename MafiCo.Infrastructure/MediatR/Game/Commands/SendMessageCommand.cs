using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Infrastructure.MediatR.Game.Commands;

public record SendMessageCommand(Guid PlayerId, string Message) : IPlayerCommand {}