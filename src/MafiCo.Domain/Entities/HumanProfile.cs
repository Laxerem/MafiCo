using MafiCo.Domain.Abstractions;

namespace MafiCo.Domain.Entities;

public class HumanProfile : BaseProfile {
    public HumanProfile(int id, string username) : base(id, username) {}
}