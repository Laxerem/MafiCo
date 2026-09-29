using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.AggregatesModel.BotAggregate.ValueObjects;
using MafiCo.Domain.AggregatesModel.LlmAggregate;

namespace MafiCo.Application.Bot.Commands;

public record ChangeBotModelCommand(BotId BotId, LlmId LlmId) : IUserCommand;
