
using SnakeGame.Specifiers;

namespace SnakeGame.Components
{
    internal class FrameWindow(int width, int height)
    {
        private Position Position { get; set; }
        private const char _sideBorderChar = '|';
        private const char _topBottomBorderChar = '-';
        private int _width = width;
        private int _height = height;

        public void Clear() => Render();

        public void SetPosition(Position position)
        {
            Position = position;
        }
        public void SetDimensions(int width, int height)
        {
            _width = width;
            _height = height;
        }
        public void Render()
        {
            var sWidth = _width;
            var sHeight = _height;
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
            return position.X > 0 && position.X < _width - 1 && position.Y > 0 && position.Y < _height - 1;
        }

        public Position GetCenterPosition()
        {
            return new(_width / 2, _height / 2);
        }
    }
}
