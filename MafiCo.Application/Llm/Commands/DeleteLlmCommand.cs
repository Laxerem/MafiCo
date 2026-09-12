using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Llm.Commands;

public record DeleteLlmCommand(Guid LlmId) : IUserCommand;