
using SnakeGame.Specifications;
using SnakeGame.Specifiers;
using SnakeGame.Utils;

namespace SnakeGame.Components;

internal class DifficultyMenu(Difficulty difficulty = Difficulty.Easy)
{
    private const string _title = "Difficulty";
    Difficulty _selectedOption = difficulty;
    public Difficulty Show(Position position)
    {
        var (fWidth, fHeight) = (14, 5);
        var difWindow = new FrameWindow(fWidth, fHeight);


        difWindow.SetPosition(position);
        difWindow.Render();
        var (x, y) = difWindow.GetCenterPosition();
        RenderTitle(position.X + x - (int)(fWidth * 0.4), position.Y);
        Options.RenderAllOptions(position.X + x - (int)(fWidth * 0.3), position.Y + y, _selectedOption);
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
            {
                _selectedOption = _selectedOption.Previous();
            }
            else if (key == ConsoleKey.DownArrow)
            {
                _selectedOption = _selectedOption.Next();
            }
            else if (key == ConsoleKey.Enter)
            {
                return _selectedOption;
            }
            Options.RenderAllOptions(position.X + x - (int)(fWidth * 0.3), position.Y + y, _selectedOption);
        }
    }
    public void RenderTitle(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(_title);
    }

    internal static class Options
    {
        public static void RenderAllOptions(int x, int y, Difficulty selectedDifficulty)
        {
            foreach (Difficulty difficulty in Enum.GetValues<Difficulty>())
            {
                bool underline = difficulty == selectedDifficulty;
                difficulty.Render(x, y + (int)difficulty - 1, underline);
            }
        }
    }
}
