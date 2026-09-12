using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Bot.Commands;

public record CreateBotCommand(string Name, Guid LlmId) : IUserCommand;
