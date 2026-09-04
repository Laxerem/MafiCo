using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Llm.Commands;

public record DeleteLlmCommand(Guid LlmId) : IAppCommand;