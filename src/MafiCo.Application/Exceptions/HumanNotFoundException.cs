namespace MafiCo.Application.Exceptions;

public class HumanNotFoundException : Exception {
    public HumanNotFoundException(string message = "Профиль не найден") : base(message) {}
}