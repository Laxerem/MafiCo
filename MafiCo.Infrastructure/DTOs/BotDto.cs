using MafiCo.Domain.DTOs;

namespace MafiCo.Infrastructure.DTOs;

public record BotDto (
    Guid Id,
    ProfileInfo ProfileInfo,
    LlmDto? LlmInfo
);