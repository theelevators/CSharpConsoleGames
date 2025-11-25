using SnakeGame.Utils;

namespace SnakeGame.Components;

internal abstract class LabeledMenu<T>(string title, int x, int y, bool useNumberIndicator)
    where T : struct, Enum
{
    internal abstract bool OnOptionSelected(T opt);
    public void Show(T selectedOption = default)
    {
        selectedOption.Next();

        Console.Clear();
        Console.CursorVisible = false;
        RenderTitle(x, y);
        RenderMenuOptions(x, y + 1, selectedOption);

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
                var shouldExit = OnOptionSelected(selectedOption);
                if (shouldExit) break;

            }
            RenderMenuOptions(x, y + 1, selectedOption);
        }

    }
    public void RenderTitle(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(title);
    }

    private void RenderMenuOptions(int x, int y, T selectedOption = default)
    {
        foreach (var opt in Enum.GetValues<T>())
        {
            opt.Render(x, y + opt.ToInt(), opt.Equals(selectedOption), useNumberIndicator);
        }
    }

}
