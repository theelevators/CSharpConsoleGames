using ConsoleGames.Core.Render.Types;
using ConsoleGames.Core.Render.Utils;

namespace BrickBreaker;

internal class Paddle
{
    private readonly static char[] _paddleChar = "=======".ToCharArray();
    private readonly static char[] _clearingBuffer = Array.NewClearingBuffer(7);
    public static int Size => _paddleChar.Length;
    public Position Position { get; private set; }

    public Paddle(int x, int y)
    {
        Position = new Position(x - (_paddleChar.Length / 2), y);
    }

    public void Move(int deltaX, Direction direction)
    {
        int newX = direction switch
        {
            Direction.Left => Position.X - deltaX,
            Direction.Right => Position.X + deltaX,
            _ => Position.X
        };

        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(_clearingBuffer);

        Position = new Position(newX, Position.Y);
        Render();
    }
    public void Render()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(_paddleChar);
    }
}
