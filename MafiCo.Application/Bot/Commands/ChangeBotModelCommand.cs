using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Bot.Commands;

public record ChangeBotModelCommand(Guid BotId, Guid LlmId) : IUserCommand;
