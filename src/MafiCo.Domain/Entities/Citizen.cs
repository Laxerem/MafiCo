using MafiCo.Domain.Abstractions;
using MafiCo.Domain.Events;
using MafiCo.Domain.Exceptions;
using MafiCo.Domain.ValueObjects;

namespace MafiCo.Domain.Entities;

public class Citizen : Player {
    public Citizen(string name, Role role) : base(name, role) {
        
    }
}