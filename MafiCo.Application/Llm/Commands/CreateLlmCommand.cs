using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Llm.Commands;

public record CreateLlmCommand(
    string ModelName,
    string Url,
    string ApiKey
) : IUserCommand;