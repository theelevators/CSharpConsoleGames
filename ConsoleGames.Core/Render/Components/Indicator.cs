using ConsoleGames.Core.Render.Utils;

namespace ConsoleGames.Core.Render.Components;

public class Indicator<T>(string? label) : Indicator(label)
{
   public Indicator<int> Create()
    {
        return new Indicator<int>(label);
    }
}



public class Indicator(string? label)
{
    private const int _asciiBase = 10;
    private readonly char[] _frameLabel = label?.ToCharArray() ?? [];
    private readonly char[] _buffer = new char[12];
    private readonly char[] clearingBuffer = Array.NewClearingBuffer(12);
    private (int left, int top) _cursorPosition = (0, 0);

    public void ReRender(ref readonly int counter)
    {
        var tmp = counter;
        SetPosition(left: _cursorPosition.left, top: _cursorPosition.top);
        Render(ref tmp);
    }
    public void SetPosition(int left, int top)
    {
        _cursorPosition = (left, top);
        Console.SetCursorPosition(left + 0, top);
        Console.Write(_frameLabel, 0, _frameLabel.Length);
    }
    public void Render(ref readonly int counter)
    {
        Console.CursorVisible = false;

        Console.SetCursorPosition(_cursorPosition.left + _frameLabel.Length, _cursorPosition.top);
        Console.Write(clearingBuffer, 0, _buffer.LengthUntilTerminator());

        counter.Itoa(_buffer, _asciiBase);

        Console.SetCursorPosition(_cursorPosition.left + _frameLabel.Length, _cursorPosition.top);
        Console.Write(_buffer, 0, _buffer.Length);
    }

    public void Dispose()
    {
        Console.SetCursorPosition(_cursorPosition.left + _frameLabel.Length, _cursorPosition.top);
        Console.Write(Array.NewClearingBuffer((label?.Length ?? 0) + 12));
    }

}
