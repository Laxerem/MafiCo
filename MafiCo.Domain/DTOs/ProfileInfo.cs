using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Domain.DTOs;
public record ProfileInfo(
    ProfileId Id,
    string Name,
    int VictoriesCount,
    int DefeatsCount
);