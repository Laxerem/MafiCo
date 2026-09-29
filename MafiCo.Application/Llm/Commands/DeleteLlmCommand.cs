using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.AggregatesModel.LlmAggregate;

namespace MafiCo.Application.Llm.Commands;

public record DeleteLlmCommand(LlmId LlmId) : IUserCommand;
