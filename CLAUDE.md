# CLAUDE.md — MafiCo

MafiCo is a single-process **terminal Mafia party game** written in .NET 9. Players (and, eventually, LLM-driven bots) join a match, get roles, and play through Day/Night phases in a Spectre.Console TUI. Profiles, bots and LLM settings are persisted to SQLite via EF Core; the live match itself runs in memory.

The project is under active development and **not feature-complete** — see "Known incomplete areas" below before assuming any flow works end-to-end.

## Key docs

There is no architecture doc or spec yet. `README.md` contains only a logo image. This file is the onboarding doc; if you produce architecture notes, create `ARCHITECTURE.md` and link it here.

---

## Tech stack

- **.NET 9** (`net9.0`), SDK pinned by `global.json` to `9.0.100` (rollForward `latestMinor`)
- **MediatR 14.2.0** — CQRS commands + notification dispatch. Referenced directly by Domain, Application and Console; Infrastructure only consumes it transitively (no handlers live there)
- **EF Core 9.0.0 + SQLite** — persistence; schema `mafico`
- **Spectre.Console 0.57.2** — the entire UI (`AnsiConsole` prompts/panels)
- **Microsoft.Extensions.Hosting 10.0.10** — generic host / DI. Note the deliberate 10.x-vs-9.x version split with EF Core
- `Nullable` and `ImplicitUsings` are **enabled** in all four projects. No `.editorconfig`, no analyzers, no `Directory.Build.props`

---

## Directory structure

```
MafiCo.Domain/           # Pure domain model. No project refs (but does reference MediatR)
├── AggregatesModel/     # One folder per aggregate: Game, Bot, Profile, Llm
│   └── GameAggregate/   #   root Game.cs + Entities/ + Events/ + Items/ (enums) + IGameRepository.cs
├── SeedWork/            # Entity, IAggregateRoot, IDomainEvent, DomainException
├── ValueObjects/        # Phase/PhaseType — DEAD, see prohibitions
├── DTOs/                # Cross-aggregate read records (PlayerInfo, ProfileInfo)
└── Exceptions/          # One DomainException subclass per aggregate

MafiCo.Application/      # → Domain. All use cases (CQRS via MediatR) + the in-memory live game runtime
├── <Feature>/           # Bot, Llm, Profile — flat Commands/ + Handlers/ per feature
├── Game/                # Bigger feature, own breakdown:
│   ├── Controllers/     #   IPlayerController implementations (DefaultController, VoterController)
│   ├── DTOs/, Notifications/  #   PublicPlayerInfo; concrete IGameNotification/IPlayerNotification/ISystemNotification records
│   ├── Mediator/        #   Commands/ + Handlers/ for player-facing commands (StartGame, SendMessage, MakeVote, GetPlayers)
│   │   └── Internal/    #     Commands/Events/Handlers not reachable from the UI (CreateProcessor, PhaseChanged, GameFinished)
│   └── GameContext.cs, GameWorker.cs, PlayerProcessor.cs, PlayerView.cs  # live per-match objects, see Key patterns
├── Interfaces/          # Commands/, Stores/, Mediator/{Access,Notifications} + IUnitOfWork, IPlayerController
└── ApplicationExtension.cs   # DI + AddMediatR() — MediatR is rooted here now, not in Infrastructure

MafiCo.Infrastructure/   # → Application, Domain. Persistence only — no use-case/CQRS code lives here anymore
├── Persistence/         # ApplicationContext, Configurations/, Repositories/, UnitOfWork
├── Mediator/            # GamePublisher — the only IGamePublisher implementation (wraps IMediator.Publish)
├── MediatorExtension.cs # DispatchDomainEventsAsync — drains Entity.Notifications into mediator.Publish
└── Migrations/

MafiCo.Console/          # → Domain, Infrastructure. Composition root + TUI (OutputType Exe)
├── Program.cs / App.cs  # host build, DI wiring, first screen selection
├── Presentation/
│   ├── Base/            # Window, ContextWindow<T>, WindowData, SwitchWindowRequest
│   ├── Features/<Name>/ # One Window subclass per screen (Menu, Profile, Bots, Llm, Game)
│   └── Styles/          # AppComponents — shared Spectre helpers
├── Configuration/       # ConfigurationController + Options bound from appconfig.json
└── System/Stores/       # UserStore (implements Application's IUserStore)
```

---

## Key patterns

### Dependency rule
Strictly inward: `Console → Infrastructure → Application → Domain`. Interfaces live in the inner layer, implementations in the outer one — repository interfaces sit next to their aggregate in Domain, implementations in `Infrastructure/Persistence/Repositories`. `MafiCo.Console.csproj` does **not** reference Application directly; Application types are visible only transitively.

### Two event pipelines — pick the right one
`IGameNotification` (Application) is the **live** pipeline: internal game moments are published as `IGameEvent`s (`PhaseChangedEvent`, `RoleAssignedEvent`) through `IGamePublisher`, picked up by MediatR notification handlers under `Game/Mediator/Internal/Handlers/` (e.g. `PhaseChangedHandler`), and turned into `IGameNotification`s sent via the singleton `GameContext` (`INotifySource`/`INotifyConsumer`). `PlayerProcessor` fans those out into a per-player unbounded `Channel<IGameNotification>` on `PlayerView`, drained by `GameSession` in the UI. `IDomainEvent` (Domain, raised via `Entity.AddNotification`, dispatched by `UnitOfWork.SaveEntitiesAsync` → `MediatorExtension.DispatchDomainEventsAsync` → `mediator.Publish`) now has real consumers too — e.g. `GameFinishedHandler` turns `GameFinishedEvent` into a `GameFinishedNotification`. Anything a player must see goes through `IGameNotification`; a bare `IDomainEvent` handler must translate it, not leak the domain type into the UI.

### CQRS via MediatR
Every use case is a command `record` + a handler class, both living in `MafiCo.Application` — flat under `<Feature>/{Commands,Handlers}` for Bot/Llm/Profile, under `Game/Mediator/{Commands,Handlers}` for player-facing Game commands. Commands not reachable from the UI (bootstrapping a `PlayerProcessor`, internal game events) live under `Game/Mediator/Internal/`. MediatR is registered once, from `ApplicationExtension.AddMediatR`, scanning the Application assembly — Infrastructure has no handlers and no `AddMediatR` call. The UI never touches aggregates directly — it sends commands via `IMediator`/`IPlayerSender` and reads notifications from the channel.

### Screen routing
`Window` subclasses raise `OnSwitchWindow`; `UserInterface` resolves the target type from DI and swaps screens. `AddUi()` reflection-scans the assembly for non-abstract `Window` types and registers them transient — **a new screen needs no DI registration**. `GameSession` is intentionally outside this router and owns its own render loop.

### DI composition
Each project exposes one `Add<Layer>()` extension, all called from `Program.cs`. `GameContext` is singleton; DbContext, repositories, `IUnitOfWork`, stores and `App` are scoped; MediatR handlers are assembly-scanned. `GameWorker` (the successor to the old `GameOrchestrator`) is deliberately **not** in DI — it takes the `Game` aggregate as a constructor argument, so it is meant to be `new`-ed per match — but nothing currently does that; see Known incomplete areas.

---

## Conventions

- **Brace style is K&R** (opening brace on the same declaration line), not Allman. Match it — the codebase is uniform on this.
- File-scoped namespaces everywhere. Classic constructors only; no primary constructors.
- `record` for events, notifications, commands and DTOs; `class` for aggregates, entities, handlers, services.
- Aggregates: `private` parameterless ctor for EF + a public ctor or `static Create(...)` factory.
- Private fields are `_camelCase`. Interfaces are `I`-prefixed (`WindowData` is a known exception).
- Errors are **exceptions, not result types**. Domain throws `DomainException` subclasses; outer layers throw framework exceptions ad hoc.
- UI-facing strings and Presentation-layer XML docs are **Russian**; code identifiers are English.
- Folder ≠ namespace in a few places (`Presentation/Styles/` → `...Presentation.Extensions`, `SeedWork/IAggregateRoot.cs` → `...Domain.Interfaces`). Grep by type name, not path.
- Repository interfaces are inconsistently sync/async — follow the style of the aggregate you are extending, not a global rule.

### Don't

- Don't add a player-visible event as an `IDomainEvent` — use an `IGameNotification`.
- Don't extend `Domain/ValueObjects/Phase.cs` or `PhaseType.cs`; the live phase model is the `GamePhase` enum in `GameAggregate/Items/`.
- Don't add MediatR commands/handlers under `MafiCo.Infrastructure` — that project has none left; all CQRS code lives in `MafiCo.Application`.
- Don't add a connection string to config expecting it to be used — `ApplicationContext.OnConfiguring` hardcodes `Data Source=../../../mafico.db`. Changing that is a deliberate refactor, not a drive-by fix.
- Don't uncomment `IGameStore`/`GameStore` or the bot branch in `CreateProcessorHandler` as a side effect of unrelated work.

### Known incomplete areas

`GameWorker` (day/night loop + role assignment + phase publishing) is never constructed or started by `StartGameHandler`, and its `IGamePublisher` dependency isn't registered in DI either — starting a game currently does not advance phases or send `RoleAssignedNotification`/`PhaseChangedNotification` at all. `RoleAssignedEvent` has no handler yet, so even once `GameWorker` runs, role assignment won't reach players until one is added (mirror `PhaseChangedHandler`). Day/night no longer changes a player's controller — `DefaultController` is assigned once at game start and never swapped. Bot/LLM play is stubbed (`CreateProcessorHandler`'s bot branch is commented out). `NotificationBuilder.Build` throws on unhandled notification types — extend its switch whenever you add a notification. Persisting an in-progress game is unimplemented (the `Game` aggregate is never added to `IGameRepository`, so `SaveEntitiesAsync` has nothing to persist).

---

## Post-change checklist

```bash
dotnet build MafiCo.sln                                    # must be warning-clean-ish; there are no analyzers to catch style
dotnet run --project MafiCo.Console/MafiCo.Console.csproj  # smoke-test the TUI

# only when the EF model changed:
dotnet ef migrations add <Name> --project MafiCo.Infrastructure --startup-project MafiCo.Console
```

There are **no automated tests and no CI** in this repository. A clean build is the only mechanical guarantee, so anything touching the game loop or notification fan-out must be verified by actually running the app.
