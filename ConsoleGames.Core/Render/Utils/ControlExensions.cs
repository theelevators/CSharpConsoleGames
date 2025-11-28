

using ConsoleGames.Core.Render.Types;

namespace ConsoleGames.Core.Render.Utils;

public static class ControlExensions
{
    extension(ConsoleKey key)
    {
        public Direction AsDirection(Direction previousDirection = Direction.None) => key switch
        {
            ConsoleKey.UpArrow when previousDirection is not Direction.Down => Direction.Up,
            ConsoleKey.DownArrow when previousDirection is not Direction.Up => Direction.Down,
            ConsoleKey.LeftArrow when previousDirection is not Direction.Right => Direction.Left,
            ConsoleKey.RightArrow when previousDirection is not Direction.Left => Direction.Right,
            _ => previousDirection,
        };
    }
}
