using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Bot.Commands;

public record GetBotsCommand() : IAppCommand<List<BotDto>>;
