namespace MafiCo.Domain.Abstractions;

public abstract class BaseProfile {
    public int Id { get; protected set; }
    public string Username {get; protected set;}
    
    public BaseProfile(int id, string username) {
        Id = id;
        Username = username;
    }
}