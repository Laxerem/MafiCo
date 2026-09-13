using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.DTOs;

namespace MafiCo.Application.Profile.Commands;

public record GetMeCommand() : IUserCommand<ProfileInfo>;
