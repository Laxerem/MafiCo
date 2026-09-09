using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Menu;
using MafiCo.Infrastructure.MediatR.Llm;
using MafiCo.Infrastructure.MediatR.Llm.Commands;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Llm;

public class LlmSettingsWindow : Window {
    private readonly IMediator _mediator;

    public LlmSettingsWindow(IMediator mediator) {
        _mediator = mediator;
    }

    public override async Task Show() {
        var models = await RenderModelsAsync();

        var choices = new Dictionary<string, Func<Task>> {
            {"Создать", async () => await CreateModelAsync()}
        };

        if (models.Count > 0) {
            choices.Add("Удалить", async () => await DeleteModelAsync(models));
        }

        choices.Add("Назад", async () => await SwitchTo<SettingsWindow>());

        await AppComponents.GiveChoice(choices);
    }

    private async Task<List<LlmDto>> RenderModelsAsync() {
        var models = await _mediator.Send(new GetLlmsCommand());
        if (models.Count == 0) {
            AppComponents.WriteInfo("Моделей пока нет\n");
            return models;
        }

        var table = new Table()
            .AddColumn("Модель")
            .AddColumn("Провайдер");

        foreach (var model in models) {
            table.AddRow(model.ModelName, model.Url);
        }

        AnsiConsole.Write(table);
        return models;
    }

    private async Task CreateModelAsync() {
        var modelName = await AppComponents.GetUserInput("Model name:");
        var providerUrl = await AppComponents.GetUserInput("Provider url:");
        var apiKey = await AppComponents.GetUserInput("Api key:");

        await RunAndReturnAsync<LlmSettingsWindow>(
            () => _mediator.Send(new CreateLlmCommand(modelName, providerUrl, apiKey)),
            "Модель добавлена!",
            "Не удалось добавить модель");
    }

    private async Task DeleteModelAsync(List<LlmDto> models) {
        var selectedModel = await AppComponents.SelectFrom(
            "Выберите модель",
            models,
            model => $"{model.ModelName} | {model.Url}");

        await RunAndReturnAsync<LlmSettingsWindow>(
            () => _mediator.Send(new DeleteLlmCommand(selectedModel.Id)),
            "Модель удалена! Боты, которые её использовали, останутся без модели.",
            "Не удалось удалить модель");
    }
}
