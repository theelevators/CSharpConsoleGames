
using BrickBreaker;
using ConsoleGames.Core.Render.Components;
using ConsoleGames.Core.Render.Types;
using System.Diagnostics;
using System.IO;

Console.OutputEncoding = System.Text.Encoding.Unicode;
//Console.OutputEncoding = System.Text.Encoding.UTF8;

var window = new FrameWindow(Console.BufferWidth, Console.BufferHeight);
window.SetPosition(new(0, 0));
window.Render();

var centerPosittion = window.GetCenterPosition();
var paddle = new Paddle(centerPosittion.X, centerPosittion.Y + centerPosittion.Y - 2);
var paddelClock = new ComponentClock(10);
var paddleDirection = Direction.Right;
var ballDirection = new CombinedDirection(X: Direction.Right, Y: Direction.Up);
var ball = new Ball(centerPosittion.X, centerPosittion.Y + centerPosittion.Y - 3);
var ballClock = new ComponentClock(30);
var (topBoundary, leftBoundary) = (1, 1);
var rightBoundary = window.Width - 3;
var bottomBoundary = window.Height - 2;
var xIncrement = ballDirection.X is Direction.Right ? 1 : -1;
var yIncrement = ballDirection.Y is Direction.Down ? 1 : -1;
ConsoleKey? pressedKey = ConsoleKey.LeftArrow;
paddle.Render();
ball.Render();
double elapsedTime = 0;
while (true)
{
    var startingTime = Stopwatch.GetTimestamp();
    Console.CursorVisible = false;

    var endTime = Stopwatch.GetElapsedTime(startingTime);

    elapsedTime += endTime.TotalMilliseconds;
    paddelClock.Add(endTime.TotalMilliseconds);
    ballClock.Add(endTime.TotalMilliseconds);


    //pressedKey = Console.ReadKey(true).Key;

    if (pressedKey is ConsoleKey.LeftArrow or ConsoleKey.RightArrow)
    {
        paddleDirection = pressedKey switch
        {
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            _ => paddleDirection
        };
    }


    if ( paddelClock.IsElapsed || pressedKey is ConsoleKey.LeftArrow or ConsoleKey.RightArrow)
    {
        paddelClock.Reset();
        if (paddle.Position.X + 7 >= window.Width - 2)
        {
            paddleDirection = Direction.Left;
        }
        else if (paddle.Position.X <= 1)
        {
            paddleDirection = Direction.Right;
        }

        paddle.Move(1, paddleDirection);
    }

    if (ballClock.IsElapsed)
    {
        ballClock.Reset();
        var (positionX, positionY) = ball.CurrentPosition;
        var (directionX, directionY) = ballDirection;

        if (positionX >= leftBoundary && positionY >= topBoundary && positionX <= rightBoundary && positionY <= bottomBoundary)
        {

            Movement movement = (directionX, directionY) switch
            {
                (Direction.Left, _) when positionX == leftBoundary && positionY != topBoundary && positionY != bottomBoundary => new Movement(new(1, yIncrement), ballDirection with { X = Direction.Right }),
                (Direction.Right, _) when positionX == rightBoundary && positionY != topBoundary && positionY != bottomBoundary => new Movement(new(-1, yIncrement), ballDirection with { X = Direction.Left }),
                (_, Direction.Up) when positionY == topBoundary && positionX != leftBoundary && positionX != rightBoundary => new Movement(new(xIncrement, 1), ballDirection with { Y = Direction.Down }),
                (_, Direction.Down) when positionY == bottomBoundary && positionX != leftBoundary && positionX != rightBoundary => new Movement(new(xIncrement, -1), ballDirection with { Y = Direction.Up }),
                (_, _) when positionY < bottomBoundary && positionX != leftBoundary && positionX != rightBoundary => new Movement(new(xIncrement, yIncrement), ballDirection),
                (_, _) when positionX < rightBoundary && positionY != topBoundary && positionY != bottomBoundary => new Movement(new(xIncrement, yIncrement), ballDirection),
                (_, _) when positionX == leftBoundary && positionY == bottomBoundary => new Movement(new(1, -1), new CombinedDirection(Direction.Right, Direction.Up)),
                (_, _) when positionX == rightBoundary && positionY == bottomBoundary => new Movement(new(-1, -1), new CombinedDirection(Direction.Left, Direction.Up)),
                (_, _) when positionX == leftBoundary && positionY == topBoundary => new Movement(new(1, 1), new CombinedDirection(Direction.Right, Direction.Down)),
                (_, _) when positionX == rightBoundary && positionY == topBoundary => new Movement(new(-1, 1), new CombinedDirection(Direction.Left, Direction.Down)),
                (_, _) when positionX == rightBoundary => new Movement(new(-1, -1), new CombinedDirection(Direction.Left, directionY)),
                (_, _) when positionX == leftBoundary => new Movement(new(1, 1), new CombinedDirection(Direction.Right, directionY)),
                _ => new Movement(new(0, 0), ballDirection with { X = Direction.None, Y = Direction.None })
            };


            if (positionY == paddle.Position.Y && positionX >= paddle.Position.X && positionX <= paddle.Position.X + Paddle.Size)
            {
                movement = (directionX, directionY) switch
                {
                    (Direction.Left, _) when positionX >= paddle.Position.X && positionX > paddle.Position.X + 5 => new Movement(new(xIncrement * 2, yIncrement), new CombinedDirection(Direction.Right, Direction.Up)),
                    (Direction.Left, _) when positionX >= paddle.Position.X && positionX > paddle.Position.X + 2 => new Movement(new(xIncrement * 2, yIncrement), new CombinedDirection(Direction.Left, Direction.Up)),
                    (Direction.Right, _) when positionX >= paddle.Position.X && positionX <= paddle.Position.X + 7 => new Movement(new(-1, yIncrement), new CombinedDirection(Direction.Left, Direction.Up)),
                    _ => movement
                };
            }
            xIncrement = (ball.CurrentPosition.X + movement.Position.X < leftBoundary, ball.CurrentPosition.X + movement.Position.X > rightBoundary) switch
            {
                (true, _) => leftBoundary + (ball.CurrentPosition.X - movement.Position.X),
                (_, true) => rightBoundary - (ball.CurrentPosition.X + movement.Position.X),
                _ => movement.Position.X
            };

            yIncrement = (ball.CurrentPosition.Y + movement.Position.Y < topBoundary, ball.CurrentPosition.Y + movement.Position.Y > bottomBoundary) switch
            {
                (true, _) => topBoundary + (ball.CurrentPosition.Y - movement.Position.Y),
                (_, true) => bottomBoundary - (ball.CurrentPosition.Y + movement.Position.Y),
                _ => movement.Position.Y
            };

            ball.Move(xIncrement, yIncrement);
            ballDirection = movement.Direction;

        }


    }


    pressedKey = Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey selectedKey ? selectedKey : null;

    if (elapsedTime > 10)
    {
        elapsedTime = 0;
    }
}
readonly record struct Movement(Position Position, CombinedDirection Direction);
readonly record struct CombinedDirection(Direction X, Direction Y);