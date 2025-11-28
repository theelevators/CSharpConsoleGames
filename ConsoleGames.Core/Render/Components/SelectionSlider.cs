

using ConsoleGames.Core.Render.Utils;

namespace ConsoleGames.Core.Render.Components;

public class SelectionSlider(int[] range, int initialSelection, string? label = null)
{
    private int _selection = initialSelection;
    private readonly int[] _range = range;
    private readonly string _label = label ?? string.Empty;
    private readonly char[] _clearingBuffer = Array.NewClearingBuffer(range.Length + (label?.Length ?? 0) + 2);
    public int Show(int x, int y)
    {
        if (_selection < 0 || _selection >= _range.Length)
        {
            _selection = 0;
        }

        var selectionLabel = new Indicator(_label);
        selectionLabel.SetPosition(x + _range.Length + 3, y);

        Render(x, y);
        while (true)
        {
            selectionLabel.Render(ref _range[_selection]);
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow)
            {
                if (_selection > 0)
                {
                    _selection--;
                    Render(x, y);
                }
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (_selection < _range.Length - 1)
                {
                    _selection++;
                    Render(x, y);
                }
            }
            else if (key == ConsoleKey.Enter)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(_clearingBuffer, 0, _clearingBuffer.Length);
                selectionLabel.Dispose();
                return _range[_selection];
            }
        }


    }

    private void Render(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write('|');
        for (int idx = 0; idx < _range.Length; idx++)
        {
            if (idx == _selection)
            {
                Console.Write('*');
            }
            else
            {
                Console.Write('-');
            }
        }
        Console.Write('|');
    }
}
