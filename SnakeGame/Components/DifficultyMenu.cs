
using ConsoleGames.Core.Render.Components;
using ConsoleGames.Core.Render.Types;
using SnakeGame.Specifications;



namespace SnakeGame.Components;

internal class DifficultyMenu(Position position, Difficulty initialDifficulty) : LabeledMenu<Difficulty>(_title, position.X, position.Y, false, initialDifficulty)
{
    private const string _title = "Difficulty";

    public override bool OnOptionSelected(Difficulty opt)
    {
        return true;
    }
}
