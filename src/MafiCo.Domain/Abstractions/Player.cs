using MafiCo.Domain.Entities;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Abstractions;

public abstract class Player {
    private BaseProfile _gameProfile;
    private Role _role;
    
    protected Player(BaseProfile gameProfile, Role role) {
        _gameProfile = gameProfile;
        _role = role;
    }
}