using MafiCo.Domain.AggregatesModel.ProfileAggregate;

namespace MafiCo.Application.Interfaces.Stores;

public interface IUserStore {
    void SetUser(ProfileId userId);
    ProfileId? GetUserId();
}
