
using SnakeGame.Specifications;

namespace SnakeGame.Utils;

internal static class RenderExtensions
{
    const string UNDERLINE = "\x1B[4m";
    const string RESET = "\x1B[0m";

    extension(MenuOption option)
    {
        public string AsRendeable() => option switch
        {
            MenuOption.StartGame => "1. Start Game",
            MenuOption.Settings => "2. Settings",
            MenuOption.Exit => "3. Exit",
            _ => string.Empty,
        };

        public void Render(int x, int y, bool underline)
        {
            var renderable = underline ? option.AsRendeable().Underlined() : option.AsRendeable();
            Console.SetCursorPosition(x, y);
            Console.Write(renderable);
        }
        public MenuOption Next()
        {
            return option switch
            {
                MenuOption.StartGame => MenuOption.Settings,
                MenuOption.Settings => MenuOption.Exit,
                MenuOption.Exit => MenuOption.StartGame,
                _ => option,
            };
        }
        public MenuOption Previous()
        {
            return option switch
            {
                MenuOption.StartGame => MenuOption.Exit,
                MenuOption.Settings => MenuOption.StartGame,
                MenuOption.Exit => MenuOption.Settings,
                _ => option,
            };
        }

    }
    extension(SettingOption option)
    {
        public string AsRendeable() => option switch
        {
            SettingOption.Difficulty => "1. Difficulty",
            SettingOption.GrowthExponent => "2. Growth Exponent",
            SettingOption.SnakeSpeed => "3. Snake Speed",
            SettingOption.NumberOfSimultaneousApples => "4. Number of Simultaneous Apples",
            SettingOption.Cancel => "5. Cancel",
            _ => string.Empty,
        };
        public void Render(int x, int y, bool underline)
        {
            var renderable = underline ? option.AsRendeable().Underlined() : option.AsRendeable();
            Console.SetCursorPosition(x, y);
            Console.Write(renderable);
        }
        public SettingOption Next()
        {
            return option switch
            {
                SettingOption.Difficulty => SettingOption.GrowthExponent,
                SettingOption.GrowthExponent => SettingOption.SnakeSpeed,
                SettingOption.SnakeSpeed => SettingOption.NumberOfSimultaneousApples,
                SettingOption.NumberOfSimultaneousApples => SettingOption.Cancel,
                SettingOption.Cancel => SettingOption.Difficulty,
                _ => option,
            };
        }
        public SettingOption Previous()
        {
            return option switch
            {
                SettingOption.Difficulty => SettingOption.NumberOfSimultaneousApples,
                SettingOption.GrowthExponent => SettingOption.Difficulty,
                SettingOption.SnakeSpeed => SettingOption.GrowthExponent,
                SettingOption.NumberOfSimultaneousApples => SettingOption.Cancel,
                SettingOption.Cancel => SettingOption.SnakeSpeed,
                _ => option,
            };
        }
    }
    extension(Difficulty level)
    {
        public string AsRendeable() => level switch
        {
            Difficulty.Easy => "~Easy~",
            Difficulty.Medium => "~Medium~",
            Difficulty.Hard => "~Hard~",
            _ => string.Empty,
        };
        public Difficulty Next()
        {
            return level switch
            {
                Difficulty.Easy => Difficulty.Medium,
                Difficulty.Medium => Difficulty.Hard,
                Difficulty.Hard => Difficulty.Easy,
                _ => level,
            };
        }
        public Difficulty Previous()
        {
            return level switch
            {
                Difficulty.Easy => Difficulty.Hard,
                Difficulty.Medium => Difficulty.Easy,
                Difficulty.Hard => Difficulty.Medium,
                _ => level,
            };
        }

        public void Render(int x, int y, bool underline)
        {
            var renderable = underline ? level.AsRendeable().Underlined() : level.AsRendeable();
            Console.SetCursorPosition(x, y);
            Console.Write(renderable);
        }
    }
    private static string Underlined(this string text) => $"{UNDERLINE}{text}{RESET}";

}
