using ConsoleGames.Core.Render.Types;
using System.Diagnostics;

namespace ConsoleGames.Core.Render.Components;

public class FrameWindow(int width, int height)
{
    private Position Position { get; set; }
    private const char _sideBorderChar = '|';
    private const char _topBottomBorderChar = '-';
    public int Width = width;
    public int Height = height;

    public void ReRender()
    {
        Dispose();
        Render();
    }
    public void Dispose()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.SetCursorPosition(x + Position.X, y + Position.Y);
                Console.Write(' ');
            }
        }
    }

    public void SetPosition(Position position)
    {
        Position = position;
    }
    public void SetDimensions(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public void Render()
    {
        var sWidth = Width;
        var sHeight = Height;
        for (int y = 0; y < sHeight; y++)
        {
            for (int x = 0; x < sWidth; x++)
            {
                if (x == 0 || x == sWidth - 1)
                {
                    Console.SetCursorPosition(x + Position.X, y + Position.Y);
                    Console.Write(_sideBorderChar);
                }
                else if (y == 0 || y == sHeight - 1)
                {
                    Console.SetCursorPosition(x + Position.X, y + Position.Y);
                    Console.Write(_topBottomBorderChar);
                }
            }
        }
        //Position = new(1, 1);
        Console.SetCursorPosition(Position.X, Position.Y);
    }

    public bool IsInsideWindow(Position position)
    {
        return position.X > 0 && position.X < Width - 1 && position.Y > 0 && position.Y < Height - 1;
    }

    public Position GetCenterPosition()
    {
        return new(Width / 2, Height / 2);
    }
}
