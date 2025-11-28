using ConsoleGames.Core.Render.Utils;
using ConsoleGames.Core.Render.Components;
using ConsoleGames.Core.Render.Types;
using SnakeGame.Specifications;




namespace SnakeGame.Components;

internal class SettingsMenu(string menuTitle, GameSettings initialSettings, int x, int y) : LabeledMenu<SettingOption>(menuTitle, x, y, useNumberIndicator: true)
{
    public override bool OnOptionSelected(SettingOption selectedOption)
    {
        int selectionIdx;
        switch (selectedOption)
        {
            case SettingOption.Difficulty:
                Console.Clear();
                var enumNames = Enum.GetNames<Difficulty>();
                var frameWindow = new FrameWindow(enumNames.Max()!.Length + 8, enumNames.Length + 2);
                frameWindow.SetPosition(new Position(x - 2, y));
                frameWindow.Render();
                
                var difficultyMenu = new DifficultyMenu(new Position(x, y), initialSettings.Difficulty);
                difficultyMenu.Show(clearScreen: false);

                difficultyMenu.Dispose();
                frameWindow.Dispose();
                initialSettings.Difficulty = difficultyMenu.SelectedOption;

                return false;
            case SettingOption.GrowthExponent:
                var growthOpts = new int[] { 1, 2, 3, 4, 5, 6, 8, 10, 12 };
                selectionIdx = Array.IndexOf(growthOpts, initialSettings.SnakeGrowthPerApple);
                var growthSelector = new SelectionSlider(growthOpts, selectionIdx);
                var selectedGrowth = growthSelector.Show(x + selectedOption.StrLength + 2, y + (int)selectedOption + 1);

                initialSettings.SnakeGrowthPerApple = selectedGrowth;
                return false;
            case SettingOption.SnakeSpeed:
                var speedOpt = new int[] { 100, 80, 60, 50, 40, 30, 20, 10 };
                selectionIdx = Array.IndexOf(speedOpt, (int)initialSettings.SnakeSpeed);
                var speedSelector = new SelectionSlider(speedOpt, selectionIdx, label: "ms: ");
                var selectedSpeed = speedSelector.Show(x + selectedOption.StrLength + 2, y + (int)selectedOption + 1);

                initialSettings.SnakeSpeed = selectedSpeed;
                return false;
            case SettingOption.NumberOfSimultaneousApples:
                var appleOpt = new int[] { 1, 2, 5, 10, 15, 20 };
                selectionIdx = Array.IndexOf(appleOpt, initialSettings.NumberOfSimultaneousApples);
                var noAppleSelector = new SelectionSlider(appleOpt, selectionIdx);
                var selectedNo = noAppleSelector.Show(x + selectedOption.StrLength + 2, y + (int)selectedOption + 1);
                initialSettings.NumberOfSimultaneousApples = selectedNo;
                return false;
            default:
                return true;

        }
    }
}
