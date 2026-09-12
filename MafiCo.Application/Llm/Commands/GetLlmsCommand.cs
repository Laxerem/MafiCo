using MafiCo.Application.Interfaces.Commands;
using MafiCo.Application.Llm;

namespace MafiCo.Application.Llm.Commands;

public record GetLlmsCommand() : IUserCommand<List<LlmDto>>;
