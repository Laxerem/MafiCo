using MafiCo.Application.Interfaces;
using MafiCo.Application.Interfaces.Commands;
using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Llm.Commands;

public record CreateLlmCommand(
    string ModelName,
    string Url,
    string ApiKey
) : IUserCommand;