using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Commands;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Bot.Commands;

public record DeleteBotCommand(Guid BotId) : IUserCommand;
