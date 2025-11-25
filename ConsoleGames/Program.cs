using SnakeGame.Components;
using SnakeGame.Specifications;


var filePath = Path.Combine(AppContext.BaseDirectory, "settings.txt");
if (!File.Exists(filePath))
    File.WriteAllText(filePath, "0\n");
var file = File.ReadAllText(filePath);
var highestScore = int.Parse(file);
var settings = GameSettings.Default;
settings.HighestScore = highestScore;
var menu = new MainMenu("=== Snake Game ===", settings);

menu.Show();
File.WriteAllText(filePath, settings.HighestScore.ToString());
Environment.Exit(0);