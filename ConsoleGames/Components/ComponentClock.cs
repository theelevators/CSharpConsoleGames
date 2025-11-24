

namespace SnakeGame.Components;

internal class ComponentClock
{
    private double _maxElapsed;
    private double _secondsElapsed;
    
    public ComponentClock(double maxElapsed)
    {
        _maxElapsed = maxElapsed;
        _secondsElapsed = 0;
    }
    public void Add(double seconds) =>_secondsElapsed += seconds;
    public bool IsElapsed => _secondsElapsed >= _maxElapsed;
    public void Reset() => _secondsElapsed = 0;

}
