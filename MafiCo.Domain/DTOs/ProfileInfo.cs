using System.ComponentModel;

namespace MafiCo.Domain.DTOs;
public record ProfileInfo(
    [ReadOnly(true)]
    Guid Id,
    [ReadOnly(true)]
    string Name
);