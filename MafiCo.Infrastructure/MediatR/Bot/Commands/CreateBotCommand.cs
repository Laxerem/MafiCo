using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Bot.Commands;

public record CreateBotCommand(string Name, Guid LlmId) : IAppCommand;
