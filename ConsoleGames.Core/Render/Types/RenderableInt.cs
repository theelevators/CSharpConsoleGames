
using ConsoleGames.Core.Render.Abstractions;
using ConsoleGames.Core.Render.Utils;

namespace ConsoleGames.Core.Render.Types;

public readonly record struct RenderableInt(int Value) : IDisplay
{
    public int CharsSize => 12; // Max chars for int32 including sign
    public void ToDisplay(char[] buffer)
    {
        Value.Itoa(buffer, 10);// ASCII base 10
    }

    public static implicit operator int(RenderableInt renderableInt) => renderableInt.Value;
    public static implicit operator RenderableInt(int value) => new(value);
}
