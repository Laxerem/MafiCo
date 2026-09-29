using MafiCo.Application.Llm;
using MafiCo.Domain.AggregatesModel.BotAggregate.ValueObjects;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Bot;

public record BotDto(
    BotId Id,
    ProfileInfo ProfileInfo,
    LlmDto? LlmInfo
);
