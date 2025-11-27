
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
    int _highestScore = 0;
    (int width, int height) _windowSize = (Console.BufferWidth, Console.BufferHeight);
    bool isValid = true;
    bool gameOver = false;
    int loopCounter = 0;
    double secondsElapsed = 0;
    Direction direction = Direction.Right;
    readonly LabelWithCounter _fpsLabel = new("FPS: ");
    readonly LabelWithCounter _scoreLabel = new("Score: ");
    readonly LabelWithCounter _highestScoreLabel = new("Highest Score: ");
    readonly FrameWindow _gameArea;
    readonly AppleManager _appleGenerator;
    readonly GameSettings _settings = GameSettings.Default;
    readonly PauseMenu _pauseMenu;
    readonly double _originalSnakeSpeed;
    private ConsoleKey? _availableKey => Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey selectedKey ? selectedKey : null;


    public Game(GameSettings gameSettings)
    {
        _settings = gameSettings;
        _originalSnakeSpeed = _settings.SnakeSpeed;
        _snakeClock = new ComponentClock(_settings.SnakeSpeed / 1000.0);
        _gameArea = new FrameWindow(_windowSize.width, _windowSize.height);
        _appleGenerator = new AppleManager(_gameArea);
        _snake = new Snake(_gameArea.GetCenterPosition(), 10);
        _pauseMenu = new PauseMenu(_settings, () =>
        {
            Console.Clear();
            _gameArea.Render();
            _snake.Render();
            _appleGenerator.AllApples().ForEach(a => a.Render());
            _scoreLabel.Render(ref _score);
        }, () =>
        {
            Console.Clear();
            isValid = false;
            gameOver = true;
        });

        _highestScore = _settings.HighestScore;

        Console.CursorVisible = false;
        Console.Title = "Snake Game";
        Console.Clear();
        _gameArea.Render();
        _fpsLabel.SetPosition(1, 0);
        _scoreLabel.SetPosition(_windowSize.width - 11, 0);
        _highestScoreLabel.SetPosition(_windowSize.width / 2 - 7, 0);
        _snake.Render();
        _appleGenerator.Generate(1).First().Render();
        _highestScoreLabel.Render(ref _highestScore);
        _originalSnakeSpeed = _settings.SnakeSpeed;
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
            var pressedKey = _availableKey;
            if (pressedKey is ConsoleKey key)
            {

                if (key is ConsoleKey.P)
                {
                    _pauseMenu.Show();
                    continue;

                }

                direction = key.AsDirection(previousDirection: direction);

                if (direction is Direction.Up or Direction.Down)
                {
                    _snakeClock.Update((_settings.SnakeSpeed * 1.2) / 1000.0);
                }
                else if (direction is Direction.Left or Direction.Right)
                {
                    _snakeClock.Update(_settings.SnakeSpeed / 1000.0);
                }
            }

            if (pressedKey is ConsoleKey.UpArrow or ConsoleKey.DownArrow or ConsoleKey.RightArrow or ConsoleKey.LeftArrow || _snakeClock.IsElapsed)
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


                    _settings.SnakeSpeed = _settings.Difficulty switch
                    {
                        Difficulty.Easy => _settings.SnakeSpeed >= 50 ? _settings.SnakeSpeed - 0.5 : _settings.SnakeSpeed,
                        Difficulty.Medium => _settings.SnakeSpeed >= 30 ? _settings.SnakeSpeed - 3 : _settings.SnakeSpeed,
                        Difficulty.Hard => _settings.SnakeSpeed >= 10 ? _settings.SnakeSpeed - 5 : _settings.SnakeSpeed,
                        _ => _settings.SnakeSpeed,
                    };


                    _snakeClock.Update(_settings.SnakeSpeed / 1000.0);
                    _snake.Grow(_settings.SnakeGrowthPerApple);
                    _appleGenerator.RemoveApple(nextHeadPosition);

                    var apples = _appleGenerator.Generate(_settings.NumberOfSimultaneousApples - _appleGenerator.AppleCount);

                    foreach (var apple in apples)
                    {
                        var newApple = apple;
                        while (_snake.IsPositionOccupied(newApple.Position))
                        {
                            var tmpApples = _appleGenerator.Generate(1);
                            newApple = tmpApples.First();
                        }
                        newApple.Render();
                    }
                    _score += 10;

                    if (_score > _settings.HighestScore)
                    {
                        _highestScore = _score;
                    }

                }

                _snake.Move(newX, newY);
                if (_snake.IsCollided)
                {
                    isValid = false;
                    gameOver = true;
                }

                _scoreLabel.Render(ref _score);
                _highestScoreLabel.Render(ref _highestScore);
            }

            //Reset every 10 seconds
            if (secondsElapsed > 10)
            {
                loopCounter = 0;
                secondsElapsed = 0;
            }


            if (gameOver)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("Game Over!");
            }
        }
        _settings.HighestScore = _highestScore;
        _settings.SnakeSpeed = _originalSnakeSpeed;
    }
}
