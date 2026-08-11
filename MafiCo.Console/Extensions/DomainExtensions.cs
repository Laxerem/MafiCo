using MafiCo.Console.Presentation;
using MafiCo.Domain.AggregatesModel.ProfileAggregate;
using MafiCo.Domain.DTOs;

namespace MafiCo.Console.Extensions;

public static class DomainExtensions {
    public static ProfileInfo ToProfileInfo(this Profile profile) {
        return new ProfileInfo(profile.Id, profile.Name);
    }
}