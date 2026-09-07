using MafiCo.Application.Interfaces.Commands;
using MafiCo.Domain.DTOs;

namespace MafiCo.Infrastructure.MediatR.Profile.Commands;

public record GetMeCommand() : IUserCommand<ProfileInfo>;
