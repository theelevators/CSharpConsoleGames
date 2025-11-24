
using SnakeGame.Specifications;
using SnakeGame.Specifiers;
using SnakeGame.Utils;
using System.Diagnostics;

namespace SnakeGame.Components;

internal class Game
{

    readonly Snake _snake;
    readonly ComponentClock _snakeClock;
    int _score = 0;
    (int width, int height) _windowSize = (Console.BufferWidth, Console.BufferHeight);
    bool isValid = true;
    bool gameOver = false;
    int loopCounter = 0;
    double secondsElapsed = 0;
    Direction direction = Direction.Right;
    readonly LabelWithCounter _fpsLabel = new("FPS: ");
    readonly LabelWithCounter _scoreLabel = new("Score: ");
    readonly FrameWindow _gameArea;
    readonly AppleManager _appleGenerator;
    readonly GameSettings _settings;
    public Game(GameSettings gameSettings)
    {
        _settings = gameSettings;
        _snakeClock = new ComponentClock(_settings.SnakeSpeed/1000.0);
        _gameArea = new FrameWindow(_windowSize.width, _windowSize.height);
        _appleGenerator = new AppleManager(_gameArea);
        _snake = new Snake(_gameArea.GetCenterPosition(), 10);

        Console.CursorVisible = false;
        Console.Title = "Snake Game";
        Console.Clear();
        _gameArea.Render();
        _fpsLabel.SetPosition(1, 0);
        _scoreLabel.SetPosition(_windowSize.width - 11, 0);
        _snake.Render();
        _appleGenerator.GenerateApple().Render();
    }

    public void Run()
    {

        while (isValid)
        {
            var startingTime = Stopwatch.GetTimestamp();
            loopCounter++;


            if (secondsElapsed > 0)
            {
                var fps = (int)(loopCounter / secondsElapsed);
                _fpsLabel.Render(ref fps);
            }


            var delta = Stopwatch.GetElapsedTime(startingTime);
            secondsElapsed += delta.TotalSeconds;
            _snakeClock.Add(delta.TotalSeconds);
            if (Console.KeyAvailable)
            {

                direction = Console.ReadKey(true).Key.AsDirection(previousDirection: direction);
            }

            if (_snakeClock.IsElapsed)
            {
                _snakeClock.Reset();
                var (newX, newY) = direction.ToDelta();


                var snakeHead = _snake.GetHeadPosition();
                var nextHeadPosition = new Position(snakeHead.X + newX, snakeHead.Y + newY);

                if (!_gameArea.IsInsideWindow(nextHeadPosition))
                {
                    isValid = false; 
                    gameOver = true;
                }


                var willEatApple = _appleGenerator.IsPositionOccupied(nextHeadPosition);
                if (willEatApple)
                {
                    _snake.Grow();
                    _appleGenerator.RemoveApple(nextHeadPosition);

                    var newApple = _appleGenerator.GenerateApple();

                    while (_snake.IsPositionOccupied(newApple.Position))
                    {
                        newApple = _appleGenerator.GenerateApple();
                    }
                    
                    newApple.Render();
                    _score += 10;

                }

                _snake.Move(newX, newY);
                if (_snake.IsCollided)
                {
                    isValid = false;
                    gameOver = true;
                }

                //Reset every 10 seconds
                if (secondsElapsed > 10)
                {
                    loopCounter = 0;
                    secondsElapsed = 0;
                }

                _scoreLabel.Render(ref _score);
            }

            if (gameOver)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("Game Over!");
            }
        }

    }
}
