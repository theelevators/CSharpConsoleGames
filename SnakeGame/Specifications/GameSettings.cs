

namespace SnakeGame.Specifications;

internal class GameSettings
{
    public const string GameTitle = "Snake Game";
    public Difficulty Difficulty
    {
        get; set
        {
            field = value;
            switch (field)
            {
                case Difficulty.Easy:
                    SnakeSpeed = 300;
                    SnakeGrowthPerApple = 1;
                    break;
                case Difficulty.Medium:
                    SnakeSpeed = 100;
                    SnakeGrowthPerApple = 2;
                    break;
                case Difficulty.Hard:
                    SnakeSpeed = 50;
                    SnakeGrowthPerApple = 4;
                    break;
            }

        }
    } = Difficulty.Easy;
    public int InitialSnakeLength { get; set; } = 10;
    public int SnakeGrowthPerApple { get; set; } = 1;
    public double SnakeSpeed { get; set; } = 100; // in milliseconds
    public int NumberOfSimultaneousApples { get; set; } = 1;
    public int HighestScore { get; set; } = 0;

    public static GameSettings Default => new();
}
