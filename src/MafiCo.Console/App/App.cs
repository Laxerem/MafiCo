using MafiCo.Application;
using MafiCo.Application.Database;
using MafiCo.Application.Orchestrators;
using MafiCo.Domain.Entities;

namespace MafiCo.Console.App;

public class App {
    private readonly GameOutput _output;
    public App() {
        _output = new GameOutput();
    }

    public async Task RunAsync() {
        MenuHandler menu = new(_output);
        while (true) {
            var input = menu.ShowMainMenu();
            switch (input) {
                case "Начать":
                    var orchestrator = new GameOrchestrator(new Game(), new GameMemory("../../../memory.db"), _output);
                    await orchestrator.SetupGameAsync();
                    break;
                case "Выйти":
                    _output.Show("Пока..");
                    return;
            }
        }
    }
}