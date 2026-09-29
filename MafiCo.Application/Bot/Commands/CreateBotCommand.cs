using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.AggregatesModel.LlmAggregate;

namespace MafiCo.Application.Bot.Commands;

public record CreateBotCommand(string Name, LlmId LlmId) : IUserCommand;
