using SnakeGame.Specifications;
using SnakeGame.Specifiers;
using SnakeGame.Utils;
using System;

namespace SnakeGame.Components;

internal class SettingsMenu(string menuTitle, GameSettings initialSettings)
{
    public string Title = menuTitle;
    private GameSettings _currentSettings = initialSettings;
    public GameSettings Show(SettingOption selectedOption = SettingOption.Difficulty)
    {
        Console.Clear();
        Console.CursorVisible = false;
        var (x, y) = (Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5);
        RenderTitle(x, y);
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
                HandleSettingSelection(selectedOption);
                return _currentSettings;
            }
            Options.RenderAllOptions(x, y + 1, selectedOption);
        }
    }
    public void RenderTitle(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(Title);
    }

    private void HandleSettingSelection(SettingOption selectedOption)
    {
        var (x, y) = (Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5);
        var selectionIdx = 0;
        switch (selectedOption)
        {
            case SettingOption.Difficulty:
                Console.Clear();
                var difficultyMenu = new DifficultyMenu(_currentSettings.Difficulty);
                _currentSettings.Difficulty = difficultyMenu.Show(new Position(x, y));
                break;
            case SettingOption.GrowthExponent:
                var growthOpts = new int[] { 1, 2, 3, 4, 5, 6, 8, 10, 12 };
                selectionIdx = Array.IndexOf(growthOpts, _currentSettings.SnakeGrowthPerApple);
                var growthSelector = new SelectionSlider(growthOpts, selectionIdx);
                var selectedGrowth = growthSelector.Show(x + selectedOption.AsRendeable().Length + 2, y + (int)selectedOption + 1);

                _currentSettings.SnakeGrowthPerApple = selectedGrowth;
                break;
            case SettingOption.SnakeSpeed:
                var speedOpt = new int[] { 100, 200, 300, 400, 500, 600, 800, 1000 };
                selectionIdx = Array.IndexOf(speedOpt, _currentSettings.SnakeSpeed);
                var speedSelector = new SelectionSlider(speedOpt, selectionIdx, label: "ms: ");
                var selectedSpeed = speedSelector.Show(x + selectedOption.AsRendeable().Length + 2, y + (int)selectedOption + 1);

                _currentSettings.SnakeSpeed = selectedSpeed;
                break;
            case SettingOption.NumberOfSimultaneousApples:
                var appleOpt = new int[] { 1, 2, 5, 10, 15, 20};
                selectionIdx = Array.IndexOf(appleOpt, _currentSettings.NumberOfSimultaneousApples);
                var noAppleSelector = new SelectionSlider(appleOpt, selectionIdx);
                var selectedNo = noAppleSelector.Show(x + selectedOption.AsRendeable().Length + 2, y + (int)selectedOption + 1);
                _currentSettings.NumberOfSimultaneousApples = selectedNo;
                break;
            default:
                return;

        }
        Show(selectedOption);
        return;
    }
    internal static class Options
    {
        public static void RenderAllOptions(int x, int y, SettingOption selectedOption = SettingOption.Difficulty)
        {
            foreach (SettingOption opt in Enum.GetValues<SettingOption>())
            {
                opt.Render(x, y + (int)opt, opt == selectedOption);
            }
        }
    }
}
