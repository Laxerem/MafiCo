using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.AggregatesModel.BotAggregate.ValueObjects;

namespace MafiCo.Application.Bot.Commands;

public record DeleteBotCommand(BotId BotId) : IUserCommand;
