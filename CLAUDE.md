# CLAUDE.md — MafiCo

MafiCo is a single-process **terminal Mafia party game** written in .NET 9. Players (and, eventually, LLM-driven bots) join a match, get roles, and play through Day/Night phases in a Spectre.Console TUI. Profiles, bots and LLM settings are persisted to SQLite via EF Core; the live match itself runs in memory.

The project is under active development and **not feature-complete** — see "Known incomplete areas" below before assuming any flow works end-to-end.

## Key docs

There is no architecture doc or spec yet. `README.md` contains only a logo image. This file is the onboarding doc; if you produce architecture notes, create `ARCHITECTURE.md` and link it here.

---

## Tech stack

- **.NET 9** (`net9.0`), SDK pinned by `global.json` to `9.0.100` (rollForward `latestMinor`)
- **MediatR 14.2.0** — CQRS commands + notification dispatch. Referenced by Domain, Infrastructure and Console
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

MafiCo.Application/      # → Domain. Use-case contracts + in-memory game runtime
├── Interfaces/          # Commands/, Controllers/, Notifications/, Stores/ + IGameOrchestrator, IEventSource, IEventConsumer
├── Game/                # GameContext, PlayerContext, PlayerProcessor — the live per-match objects
└── Notifications/       # Concrete notification records sent to players

MafiCo.Infrastructure/   # → Application, Domain. Everything impure
├── MediatR/<Feature>/   # Commands/ + Handlers/ per feature (Bot, Game, Llm, Profile, System)
│   └── Game/GameOrchestrator.cs   # the game loop — lives HERE, not in Application
├── Persistence/         # ApplicationContext, Configurations/, Repositories/, UnitOfWork
├── Controllers/         # IPlayerController implementations
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
`IGameNotification` (Application) is the **live** pipeline: raised by `GameOrchestrator`, pushed through the singleton `GameContext` (`IEventConsumer`/`IEventSource`), fanned out by `PlayerProcessor` into a per-player unbounded `Channel<IGameNotification>` on `PlayerContext`, drained by `GameSession` in the UI. `IDomainEvent` (Domain, raised via `Entity.AddNotification`, dispatched by `UnitOfWork.SaveEntitiesAsync` → `DispatchDomainEventsAsync`) is currently **inert** — its only handler throws `NotImplementedException` and live games are never saved. Anything a player must see goes through notifications.

### CQRS via MediatR
Every use case is a command `record` + a handler class under `Infrastructure/MediatR/<Feature>/`. The UI never touches aggregates or the orchestrator directly — it sends commands via `IMediator` and reads notifications from the channel.

### Screen routing
`Window` subclasses raise `OnSwitchWindow`; `UserInterface` resolves the target type from DI and swaps screens. `AddUi()` reflection-scans the assembly for non-abstract `Window` types and registers them transient — **a new screen needs no DI registration**. `GameSession` is intentionally outside this router and owns its own render loop.

### DI composition
Each project exposes one `Add<Layer>()` extension, all called from `Program.cs`. `GameContext` is singleton; DbContext, repositories, `IUnitOfWork`, stores and `App` are scoped; MediatR handlers are assembly-scanned. `GameOrchestrator` is **not** in DI — it is `new`-ed per match inside `StartGameHandler`.

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
- Don't register `IGameOrchestrator` in DI; it is constructed per match by design.
- Don't add a connection string to config expecting it to be used — `ApplicationContext.OnConfiguring` hardcodes `Data Source=../../../mafico.db`. Changing that is a deliberate refactor, not a drive-by fix.
- Don't uncomment `IGameStore`/`GameStore` or the bot branch in `CreateProcessorHandler` as a side effect of unrelated work.

### Known incomplete areas

Night phase is a no-op in `GameOrchestrator.ProcessPhase`; the loop advances on a hardcoded 10 s delay and never calls `Game.NextPhase()`. Bot/LLM play is stubbed. `NotificationBuilder.Build` throws on unhandled notification types — extend its switch whenever you add a notification. Persisting an in-progress game is unimplemented.

---

## Post-change checklist

```bash
dotnet build MafiCo.sln                                    # must be warning-clean-ish; there are no analyzers to catch style
dotnet run --project MafiCo.Console/MafiCo.Console.csproj  # smoke-test the TUI

# only when the EF model changed:
dotnet ef migrations add <Name> --project MafiCo.Infrastructure --startup-project MafiCo.Console
```

There are **no automated tests and no CI** in this repository. A clean build is the only mechanical guarantee, so anything touching the game loop or notification fan-out must be verified by actually running the app.
