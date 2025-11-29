

using ConsoleGames.Core.Render.Types;
using ConsoleGames.Core.Render.Utils;

namespace BrickBreaker;

internal class Ball(int x, int y)
{
    public Position PreviousPosition = new Position(x, y);
    public Position CurrentPosition = new Position(x, y);
    private const char _ball = '\u26AA';
    private static char[] _clearingBuffer = Array.NewClearingBuffer(1);

    public void Move(int deltaX, int deltaY)
    {
        PreviousPosition = CurrentPosition;
        Console.SetCursorPosition(CurrentPosition.X, CurrentPosition.Y);
        Console.Write(_clearingBuffer);
        CurrentPosition = new Position(CurrentPosition.X + deltaX, CurrentPosition.Y + deltaY);
        Render();
    }

    public void Render()
    {
        Console.SetCursorPosition(CurrentPosition.X, CurrentPosition.Y);
        Console.Write(_ball);
    }
}
