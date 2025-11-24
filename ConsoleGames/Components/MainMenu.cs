
using SnakeGame.Specifications;
using SnakeGame.Utils;

namespace SnakeGame.Components
{
    internal class MainMenu(string gameTitle)
    {
        public string GameTitle = gameTitle;
        private GameSettings _currentSettings = GameSettings.Default;

        public void Show()
        {
            Console.Clear();
            Console.CursorVisible = false;
            var (x, y) = (Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5);
            RenderTitle(x, y);
            var selectedOption = MenuOption.StartGame;
            Options.RenderAllOptions(x, y + 1, selectedOption);

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    selectedOption = selectedOption.Previous();
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedOption = selectedOption.Next();
                }
                else if (key == ConsoleKey.Enter)
                {
                    HandleMenuSelection(selectedOption);
                    
                    if (selectedOption == MenuOption.Exit)
                    {
                        Console.Clear();
                        Console.WriteLine("Exiting the game. Goodbye!");
                        break;
                    }

                    if (selectedOption == MenuOption.StartGame)
                    {
                        var game = new Game(_currentSettings);
                        game.Run();
                    }
                }
                Options.RenderAllOptions(x, y + 1, selectedOption);
            }

        }
        public void RenderTitle(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(GameTitle);
        }

        private GameSettings? HandleMenuSelection(MenuOption selectedOption) => selectedOption switch
        {
            MenuOption.StartGame => _currentSettings,
            MenuOption.Settings => HandleSettingsSelection(),
            _ => null,
        };

        private GameSettings HandleSettingsSelection()
        {
            var settingsMenu = new SettingsMenu("Snake Game - Settings", _currentSettings);
            _currentSettings = settingsMenu.Show();
            Console.Clear();
            return _currentSettings;
        }
    }

    internal static class Options
    {
        public static void RenderAllOptions(int x, int y, MenuOption selectedOption = MenuOption.StartGame)
        {
            foreach (MenuOption opt in Enum.GetValues<MenuOption>())
            {
                opt.Render(x, y + (int)opt, opt == selectedOption);
            }
        }
    }
}

