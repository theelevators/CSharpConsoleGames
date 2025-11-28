namespace ConsoleGames.Core.Render.Components;

public class ComponentClock(double maxElapsed)
{
    private double _maxElapsed = maxElapsed;
    private double _secondsElapsed = 0;

    public void Add(double seconds) =>_secondsElapsed += seconds;
    public bool IsElapsed => _secondsElapsed >= _maxElapsed;
    public void Reset() => _secondsElapsed = 0;
    public void Update(double maxElapsed) => _maxElapsed = maxElapsed;

}
