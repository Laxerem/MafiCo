using MafiCo.Application.Llm;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Bot;

public record BotDto(
    Guid Id,
    ProfileInfo ProfileInfo,
    LlmDto? LlmInfo
);
