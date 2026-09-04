using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Bot.Commands;

public record DeleteBotCommand(Guid BotId) : IAppCommand;
