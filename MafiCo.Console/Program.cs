using MafiCo.Console.LobbyContext.Windows;
using MafiCo.Console.Presentation;

var userInterface = new UserInterface(new MenuWindow());
await userInterface.StartRetention();