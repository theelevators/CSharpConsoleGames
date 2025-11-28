
using ConsoleGames.Core.Render.Types;

namespace ConsoleGames.Core.Render.Utils;

public static class RenderExtensions
{
    const string UNDERLINE = "\x1B[4m";
    const string RESET = "\x1B[0m";

    extension(Position position)
    {
        public void Render(char item, ConsoleColor foreground = ConsoleColor.White)
        {
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(item);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Erase()
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(' ');
        }
    }
    extension<T>(T enumValue)
    where T : struct, Enum
    {

        public int Length => enumValue.AsRendeable(false).Length;
        public int StrLength => enumValue.AsRendeable(true).Length;

        internal string AsRendeable(bool useNumberIndicator = true)
        {
            var opt = enumValue.ToInt();
            var name = Enum.GetName(enumValue);

            if (useNumberIndicator)
            {
                return $"{opt}. {name}";
            }
            else
            {
                return $"{name}";
            }
        }

        public void Render(int x, int y, bool underline, bool useNumberIndicator = false)
        {
            var renderable = underline ? enumValue.AsRendeable(useNumberIndicator).Underlined() : enumValue.AsRendeable(useNumberIndicator);
            Console.SetCursorPosition(x, y);
            Console.Write(renderable);
        }
        public T Next()
        {

            var values = Enum.GetValues<T>();
            var index = Array.IndexOf(values, enumValue);
            var nextIndex = (index + 1) % values.Length;
            return values[nextIndex];
        }
        public T Previous()
        {
            var values = Enum.GetValues<T>();
            var index = Array.IndexOf(values, enumValue);
            var previousIndex = (index - 1 + values.Length) % values.Length;
            return values[previousIndex];

        }
    }
    private static string Underlined(this string text) => $"{UNDERLINE}{text}{RESET}";

}
