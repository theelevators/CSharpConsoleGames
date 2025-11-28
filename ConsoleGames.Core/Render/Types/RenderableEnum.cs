using ConsoleGames.Core.Render.Abstractions;
using ConsoleGames.Core.Render.Utils;

namespace ConsoleGames.Core.Render.Types;

public readonly record struct RenderableEnum<TEnum>(TEnum Value) : IDisplay
    where TEnum : struct, Enum
{
    public int CharsSize => throw new NotImplementedException();

    public void ToDisplay(char[] buffer)
    {
        var strEnum = $"{Value}".ToCharArray();

        for(int i = 0; i < strEnum.Length; i++)
        {
            buffer[i] = strEnum[i];
        }
    }

    public int ToInt() => Value.ToInt();
    public static implicit operator TEnum(RenderableEnum<TEnum> renderableEnum) => renderableEnum.Value;
    public static implicit operator RenderableEnum<TEnum>(TEnum enumValue) => new(enumValue);
}
