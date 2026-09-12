using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Bot.Commands;

public record DeleteBotCommand(Guid BotId) : IUserCommand;
