using MafiCo.Domain.DTOs;
using MafiCo.Infrastructure.MediatR.Llm;

namespace MafiCo.Infrastructure.MediatR.Bot;

public record BotDto(
    Guid Id,
    ProfileInfo ProfileInfo,
    LlmDto? LlmInfo
);
