using ConsoleGames.Core.Render.Types;
using ConsoleGames.Core.Render.Utils;

namespace ConsoleGames.Core.Render.Components;

public abstract class LabeledMenu<T>(string title, int x, int y, bool useNumberIndicator, T startOption = default)
    where T : struct, Enum
{
    static readonly T[] _options = Enum.GetValues<T>();
    public T SelectedOption = startOption;
    public abstract bool OnOptionSelected(T opt);
    public void Show(bool clearScreen = true)
    {
        if (!_options.Contains(SelectedOption))
            SelectedOption = SelectedOption.Next();
        if (clearScreen)
            Console.Clear();
        Console.CursorVisible = false;
        RenderTitle(x, y);
        RenderMenuOptions(x, y + 1, SelectedOption);

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
            {
                SelectedOption = SelectedOption.Previous();
            }
            else if (key == ConsoleKey.DownArrow)
            {
                SelectedOption = SelectedOption.Next();
            }
            else if (key == ConsoleKey.Enter)
            {
                var shouldExit = OnOptionSelected(SelectedOption);
                if (shouldExit) break;

            }
            RenderMenuOptions(x, y + 1, SelectedOption);
        }

    }
    public void RenderTitle(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(title);
    }
    public void Dispose()
    {
        Console.SetCursorPosition(x, y);
        Console.Write(Array.NewClearingBuffer(title.Length));
        foreach (var opt in _options)
        {
            Console.SetCursorPosition(x, y + opt.ToInt() + 1);
            Console.Write(Array.NewClearingBuffer(opt.StrLength));
        }
    }

    private void RenderMenuOptions(int x, int y, T selectedOption = default)
    {
        foreach (var opt in _options)
        {
            opt.Render(x, y + opt.ToInt(), opt.Equals(selectedOption), useNumberIndicator);
        }
    }

}
