using MafiCo.Console.Presentation.Base;
using MafiCo.Console.Presentation.Extensions;
using MafiCo.Console.Presentation.Features.Menu;
using MafiCo.Infrastructure.MediatR.Bot;
using MafiCo.Infrastructure.MediatR.Bot.Commands;
using MafiCo.Infrastructure.MediatR.Llm;
using MafiCo.Infrastructure.MediatR.Llm.Commands;
using MediatR;
using Spectre.Console;

namespace MafiCo.Console.Presentation.Features.Bots;

public class BotSettingsWindow : Window {
    private readonly IMediator _mediator;

    public BotSettingsWindow(IMediator mediator) {
        _mediator = mediator;
    }

    public override async Task Show() {
        var bots = await RenderBotsAsync();

        var choices = new Dictionary<string, Func<Task>> {
            {"Создать", async () => await CreateBotAsync()}
        };

        if (bots.Count > 0) {
            choices.Add("Редактировать", async () => await EditBotAsync(bots));
        }

        choices.Add("Назад", async () => await SwitchTo<SettingsWindow>());

        await AppComponents.GiveChoice(choices);
    }

    private async Task<List<BotDto>> RenderBotsAsync() {
        var bots = await _mediator.Send(new GetBotsCommand());
        if (bots.Count == 0) {
            AppComponents.WriteInfo("Ботов пока нет\n");
            return bots;
        }

        var table = new Table()
            .AddColumn("Имя")
            .AddColumn("Модель");

        foreach (var bot in bots) {
            table.AddRow(bot.ProfileInfo.Name, bot.LlmInfo?.ModelName ?? "— модель удалена —");
        }

        AnsiConsole.Write(table);
        return bots;
    }

    private async Task CreateBotAsync() {
        var llmModels = await GetAvailableModelsAsync();
        if (llmModels is null) {
            return;
        }

        var selectedLlm = await AppComponents.SelectFrom("Выберите модель", llmModels, llm => llm.ModelName);
        var name = await AppComponents.GetUserInput("Имя бота");

        await RunAndReturnAsync<BotSettingsWindow>(
            () => _mediator.Send(new CreateBotCommand(name, selectedLlm.Id)),
            "Бот создан!",
            "Не удалось создать бота");
    }

    private async Task EditBotAsync(List<BotDto> bots) {
        var selectedBot = await AppComponents.SelectFrom(
            "Выберите бота",
            bots,
            bot => $"{bot.ProfileInfo.Name} | {bot.LlmInfo?.ModelName ?? "модель удалена"} | {bot.LlmInfo?.Url ?? "-"}");

        await AppComponents.GiveChoice(new() {
            {"Удалить", async () => await DeleteBotAsync(selectedBot)},
            {"Изменить модель", async () => await ChangeBotModelAsync(selectedBot)},
            {"Назад", async () => await SwitchTo<BotSettingsWindow>()}
        });
    }

    private async Task DeleteBotAsync(BotDto bot) {
        await RunAndReturnAsync<BotSettingsWindow>(
            () => _mediator.Send(new DeleteBotCommand(bot.Id)),
            "Бот удалён!",
            "Не удалось удалить бота");
    }

    private async Task ChangeBotModelAsync(BotDto bot) {
        var llmModels = await GetAvailableModelsAsync();
        if (llmModels is null) {
            return;
        }

        var selectedLlm = await AppComponents.SelectFrom("Выберите модель", llmModels, llm => llm.ModelName);

        await RunAndReturnAsync<BotSettingsWindow>(
            () => _mediator.Send(new ChangeBotModelCommand(bot.Id, selectedLlm.Id)),
            "Модель изменена!",
            "Не удалось изменить модель");
    }

    private async Task<List<LlmDto>?> GetAvailableModelsAsync() {
        var llmModels = await _mediator.Send(new GetLlmsCommand());
        if (llmModels.Count > 0) {
            return llmModels;
        }

        AppComponents.WriteInfo("Сначала создайте модель");
        await Task.Delay(2000);
        await SwitchTo<BotSettingsWindow>();
        return null;
    }
}
