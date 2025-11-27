
namespace SnakeGame.Specifiers;

internal enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}

static internal class DirectionExtensions
{
    extension(Direction direction)
    {
        public (int deltaX, int deltaY) ToDelta() => direction switch
        {
            Direction.Up => (0, -1),
            Direction.Down => (0, 1),
            Direction.Left => (-1, 0),
            Direction.Right => (1, 0),
            _ => (0, 0)
        };
    }
}
