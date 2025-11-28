using ConsoleGames.Core.Render.Components;
using SnakeGame.Specifications;

namespace SnakeGame.Components;

internal class PauseMenu(GameSettings settings,Action OnResume, Action OnExit) : LabeledMenu<PauseOption>("Game Paused", Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5, useNumberIndicator: true)
{
    public override bool OnOptionSelected(PauseOption selectedOption)
    {

        if (selectedOption == PauseOption.Resume)
        {
            OnResume();
        }
        else if (selectedOption == PauseOption.Settings)
        {
            var (x, y) = (Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5);
            var settingsMenu = new SettingsMenu("Snake Game - Settings", settings, x, y);
            settingsMenu.Show();
            OnResume();
            
        }
        else if (selectedOption == PauseOption.ExitToMainMenu)
        {
            OnExit();
        }

        return true;
    }
}
