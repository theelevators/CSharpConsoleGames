
using SnakeGame.Specifiers;

namespace SnakeGame.Components;

internal class AppleManager
{
    private readonly FrameWindow _gameArea;
    private readonly Random _random = new();
    private readonly HashSet<Position> _occupiedPositions = [];
    public AppleManager(FrameWindow gameArea)
    {
        _gameArea = gameArea;
    }

    public List<Apple> Generate(int count)
    {
        var apples = new List<Apple>();
        for (int i = 0; i < count; i++)
        {
            apples.Add(GenerateApple());
        }
        return apples;
    }

    public List<Apple> AllApples()
    {
        var apples = new List<Apple>();
        foreach (var pos in _occupiedPositions)
        {
            apples.Add(new Apple(pos));
        }
        return apples;
    }

    private Apple GenerateApple()
    {
        int x, y;
        do
        {
            x = _random.Next(1, Console.BufferWidth - 1);
            y = _random.Next(1, Console.BufferHeight - 1);
        } while (!_gameArea.IsInsideWindow(new(x, y)) && !IsPositionOccupied(new(x, y)));
        var position = new Position(x, y);
        _occupiedPositions.Add(position);
        return new Apple(position);
    }

    public int AppleCount => _occupiedPositions.Count;
    public bool IsPositionOccupied(Position position)
    {

        foreach (var pos in _occupiedPositions)
        {
            if (pos == position)
                return true;

        }
        return false;
    }
    public void RemoveApple(Position position) => _occupiedPositions.Remove(position);
}
