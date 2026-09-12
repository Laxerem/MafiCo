using MafiCo.Application.Interfaces.Commands;

namespace MafiCo.Application.Profile.Commands;

public record CreateProfileCommand(string Name) : IUserCommand;
