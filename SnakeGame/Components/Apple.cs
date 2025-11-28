using ConsoleGames.Core.Render.Types;

namespace SnakeGame.Components;

internal class Apple(Position position)
{
    private const char _value  = (char)248;
    public Position Position { get; private set; } = position;

    public void Render()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(_value);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

