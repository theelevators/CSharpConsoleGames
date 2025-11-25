using SnakeGame.Specifications;
using SnakeGame.Specifiers;
using SnakeGame.Utils;
using System;

namespace SnakeGame.Components;

internal class SettingsMenu(string menuTitle, GameSettings initialSettings, int x, int y) : LabeledMenu<SettingOption>(menuTitle, x, y, useNumberIndicator: true)
{
    internal override bool OnOptionSelected(SettingOption selectedOption)
    {
        int selectionIdx;
        switch (selectedOption)
        {
            case SettingOption.Difficulty:
                Console.Clear();
                var difficultyMenu = new DifficultyMenu(initialSettings.Difficulty);
                initialSettings.Difficulty = difficultyMenu.Show(new Position(x, y));
                break;
            case SettingOption.GrowthExponent:
                var growthOpts = new int[] { 1, 2, 3, 4, 5, 6, 8, 10, 12 };
                selectionIdx = Array.IndexOf(growthOpts, initialSettings.SnakeGrowthPerApple);
                var growthSelector = new SelectionSlider(growthOpts, selectionIdx);
                var selectedGrowth = growthSelector.Show(x + selectedOption.AsRendeable().Length + 2, y + (int)selectedOption + 1);

                initialSettings.SnakeGrowthPerApple = selectedGrowth;
                break;
            case SettingOption.SnakeSpeed:
                var speedOpt = new int[] { 100, 80, 60, 50, 40, 30, 20, 10 };
                selectionIdx = Array.IndexOf(speedOpt, (int)initialSettings.SnakeSpeed);
                var speedSelector = new SelectionSlider(speedOpt, selectionIdx, label: "ms: ");
                var selectedSpeed = speedSelector.Show(x + selectedOption.AsRendeable().Length + 2, y + (int)selectedOption + 1);

                initialSettings.SnakeSpeed = selectedSpeed;
                break;
            case SettingOption.NumberOfSimultaneousApples:
                var appleOpt = new int[] { 1, 2, 5, 10, 15, 20 };
                selectionIdx = Array.IndexOf(appleOpt, initialSettings.NumberOfSimultaneousApples);
                var noAppleSelector = new SelectionSlider(appleOpt, selectionIdx);
                var selectedNo = noAppleSelector.Show(x + selectedOption.AsRendeable().Length + 2, y + (int)selectedOption + 1);
                initialSettings.NumberOfSimultaneousApples = selectedNo;
                break;
            default:
                return true;

        }
        Show(selectedOption);
        return true;
    }
}
