using MafiCo.Application.Interfaces;

namespace MafiCo.Infrastracture.DTOs;

public record Person(
    IPlayerBrain Brain,
    string Username
);