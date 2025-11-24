
using SnakeGame.Utils;

namespace SnakeGame.Components;

internal class LabelWithCounter
{
    private const int _asciiBase = 10;
    private readonly char[] _frameLabel;
    private  readonly char[] _buffer = new char[12];
    private  readonly char[] clearingBuffer = new char[23];
    private  (int left, int top) _cursorPosition = (0,0);

    public LabelWithCounter(string label)
    {
        _frameLabel = label.ToCharArray();
    }
    public  void SetPosition(int left, int top)
    {
        _cursorPosition = (left, top);
        Console.SetCursorPosition(left + 0, top);
        Console.Write(_frameLabel, 0, _frameLabel.Length);
    }
    public void Render(ref readonly int counter)
    {
        Console.CursorVisible = false;
        //Move the cursor after the label
        _buffer.FillClearingBuffer(clearingBuffer);
        Console.SetCursorPosition(_cursorPosition.left + _frameLabel.Length, _cursorPosition.top);
        Console.Write(clearingBuffer, 0, clearingBuffer.Length);
        counter.Itoa(_buffer, _asciiBase);
        Console.SetCursorPosition(_cursorPosition.left + _frameLabel.Length, _cursorPosition.top);
        Console.Write(_buffer, 0, _buffer.Length);
    }

}
