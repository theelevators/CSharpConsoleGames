namespace ConsoleGames.Core.Render.Abstractions;

public interface IDisplay
{
    int CharsSize { get; }
    void ToDisplay(char[] buffer);
}
