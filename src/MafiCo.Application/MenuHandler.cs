using MafiCo.Application.Orchestrators;
using MafiCo.Domain.Entities;
using Spectre.Console;

namespace MafiCo.Application;

public class MenuHandler {
    private readonly GameOutput _output;
    public MenuHandler(GameOutput output) {
        _output = output;
    }
    
    public string ShowMainMenu() {
        return _output.WaitChoice("MafiCo", ["Начать", "Выйти"]);
    }
}