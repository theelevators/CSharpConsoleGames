using SnakeGame.Specifications;

namespace SnakeGame.Components;

internal class PauseMenu(Action OnResume, Action OnExit) : LabeledMenu<PauseOption>("Game Paused", Console.BufferWidth / 2 - 10, Console.BufferHeight / 2 - 5, useNumberIndicator: true)
{
    internal override bool OnOptionSelected(PauseOption selectedOption)
    {

        if (selectedOption == PauseOption.Resume)
        {
            OnResume();
        }
        else if (selectedOption == PauseOption.ExitToMainMenu)
        {
            OnExit();
        }

        return true;
    }
}
