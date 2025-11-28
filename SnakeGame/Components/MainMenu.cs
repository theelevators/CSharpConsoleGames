
using ConsoleGames.Core.Render.Components;
using SnakeGame.Specifications;


namespace SnakeGame.Components;

internal class MainMenu(string gameTitle, GameSettings gameSettings) : LabeledMenu<MenuOption>(gameTitle, Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5, useNumberIndicator: true)
{

    public override bool OnOptionSelected(MenuOption selectedOption)
    {
        HandleMenuSelection(selectedOption);

        if (selectedOption == MenuOption.Exit)
        {
            Console.Clear();
            Console.WriteLine("Exiting the game. Goodbye!");
            return true;
        }

        if (selectedOption == MenuOption.StartGame)
        {
            var game = new Game(gameSettings);
            game.Run();
        }
        return false;
    }

    private GameSettings? HandleMenuSelection(MenuOption selectedOption) => selectedOption switch
    {
        MenuOption.StartGame => gameSettings,
        MenuOption.Settings => HandleSettingsSelection(),
        _ => null,
    };

    private GameSettings HandleSettingsSelection()
    {
        var (x, y) = (Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5);
        var settingsMenu = new SettingsMenu("Snake Game - Settings", gameSettings, x, y);
        settingsMenu.Show();
        Console.Clear();
        return gameSettings;
    }
}
