using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Llm.Commands;

public record GetLlmsCommand() : IAppCommand<List<LlmDto>>;
