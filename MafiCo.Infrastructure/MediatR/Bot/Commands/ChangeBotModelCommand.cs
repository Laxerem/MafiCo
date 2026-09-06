using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Commands;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Bot.Commands;

public record ChangeBotModelCommand(Guid BotId, Guid LlmId) : IUserCommand;
