
using SnakeGame.Specifiers;
using SnakeGame.Utils;

namespace SnakeGame.Components;

internal class Snake
{
    private const char _snakeBodyChar = 'O';
    private readonly LinkedList<Position> _snakeBodyPositions = new();

    public Snake(Position position = default, int initialLength = 1)
    {
        if (initialLength < 1)
            throw new ArgumentException("Paramater cannot be less than 1", nameof(initialLength));
        for (int i = 0; i < initialLength; i++)
        {
            _snakeBodyPositions.AddLast(new Position(position.X - i, position.Y));
        }
    }
    public void Render()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var (left, top) in _snakeBodyPositions)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(_snakeBodyChar);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void ClearSnake()
    {
        foreach (var (left, top) in _snakeBodyPositions)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(' ');
        }
    }

    public void Grow(int count)
    {
        while (count > 0)
        {
            var tail = _snakeBodyPositions.Last!.Value;
            _snakeBodyPositions.AddLast(tail);
            count--;
        }
    }

    public void Grow()
    {
        var tail = _snakeBodyPositions.Last!.Value;
        _snakeBodyPositions.AddLast(tail);
    }
    public Position GetHeadPosition()
    {
        var (x, y) = _snakeBodyPositions.First!.Value;
        return new Position(x, y);
    }

    public void Move(int deltaX, int deltaY)
    {
        var head = _snakeBodyPositions.First!.Value;
        var newHead = new Position(head.X + deltaX, head.Y + deltaY);
        _snakeBodyPositions.AddFirst(newHead);
        var oldTail = _snakeBodyPositions.Last!.Value;
        _snakeBodyPositions.RemoveLast();
        newHead.Render(_snakeBodyChar, ConsoleColor.DarkGreen);
        oldTail.Erase();
    }
    public bool IsCollided
    {
        get
        {
            var head = _snakeBodyPositions.First!.Value;
            foreach (var segment in _snakeBodyPositions.Skip(1))
            {
                if (head == segment)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public bool IsPositionOccupied(Position position) => _snakeBodyPositions.Contains(position);


}
