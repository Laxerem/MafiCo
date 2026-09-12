using MafiCo.Application.Bot;
using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Bot.Commands;

public record GetBotsCommand() : IUserCommand<List<BotDto>>;
