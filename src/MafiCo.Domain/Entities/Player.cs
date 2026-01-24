using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Entities;

public class Player {
    private string _name;
    private Role _role;
    
    private Player(string name, Role role) {
        _name = name;
        _role = role;
    }

    public static Player Create(string name, Role role) {
        return new Player(name, role);
    }
}