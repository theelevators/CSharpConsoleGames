

namespace SnakeGame.Specifications;

internal class GameSettings
{
    public const string GameTitle = "Snake Game";
    public Difficulty Difficulty { get; set; } = Difficulty.Easy;
    public int InitialSnakeLength { get; set; } = 10;
    public int SnakeGrowthPerApple { get; set; } = 1;
    public int SnakeSpeed { get; set;  } = 100; // in milliseconds
    public int NumberOfSimultaneousApples { get; set; } = 1;

    public static GameSettings Default => new();
}
