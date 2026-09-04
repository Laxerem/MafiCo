using MafiCo.Infrastructure.Interfaces;

namespace MafiCo.Infrastructure.MediatR.Profile.Commands;

public record CreateProfileCommand(string Name) : IAppCommand;
