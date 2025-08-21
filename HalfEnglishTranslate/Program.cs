using Microsoft.Win32;
using Spectre.Console;
Console.Title = "Deadlock-HalfEnglishTranslate";

StarterInfo();
string deadlockPath = @"C:\Program Files (x86)\Steam\steamapps\common\Deadlock";
bool deadlockExists = true;
string[] actions = ["1", "2", "3"];
Console.ForegroundColor = ConsoleColor.Red;

string registryPath = @"Software\HalfEnglishTranslate"; // раздел в реестре
string valueName = "DeadlockPath";           // ключ внутри раздела

if (!Directory.Exists(deadlockPath))
{
    // Читаем данные
    var key = Registry.CurrentUser.OpenSubKey(registryPath);
    if (key != null)
    {
        deadlockPath = key.GetValue(valueName)?.ToString() ?? deadlockPath;
        key.Close();
    }
}

do
{
    Console.Clear();
    StarterInfo();
    Console.ForegroundColor = ConsoleColor.Red;
    if (!Directory.Exists(deadlockPath))
    {
        Console.WriteLine($"Ошибка: директория с игрой не корректна ({deadlockPath})");
        deadlockExists = false;

        Console.WriteLine("Введите путь до корневой папки с игрой:");
        Console.ForegroundColor = ConsoleColor.Gray;
        string path = Console.ReadLine().ToString();

        while (string.IsNullOrEmpty(path))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Вы ввели пустое значение. Повторите ввод:");
            Console.ForegroundColor = ConsoleColor.Gray;
            path = Console.ReadLine().ToString();
        }

        if (path != null)
        {
            // Сохраняем данные
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);
            key.SetValue(valueName, path);
            key.Close();
            deadlockPath = path;
        }
    }
    else
    {
        deadlockExists = true;
    }
}
while (!deadlockExists);

Console.Clear();
StarterInfo();
ActionChoose();
string actionNum = Console.ReadLine().ToString();

while (!actions.Contains(actionNum))
{
    Console.Clear();
    StarterInfo();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Введенно неверное значение");
    ActionChoose();
    actionNum = Console.ReadLine().ToString();
}

Console.Clear();
StarterInfo();
await ProgressBar();

if (actionNum == "1" || actionNum == "3")
{
    string pathHeroRU = deadlockPath + @"\game\citadel\resource\localization\citadel_gc_hero_names\citadel_gc_hero_names_russian.txt";
    string pathHeroEN = deadlockPath + @"\game\citadel\resource\localization\citadel_gc_hero_names\citadel_gc_hero_names_english.txt";

    try
    {
        File.Copy(pathHeroEN, pathHeroRU, true);
        Console.Clear();
        StarterInfo();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Успешно");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Произошла ошибка: {ex.Message}");
    }
}
if (actionNum == "2" || actionNum == "3")
{
    string pathModRU = deadlockPath + @"\game\citadel\resource\localization\citadel_gc_mod_names\citadel_gc_mod_names_russian.txt";
    string pathModEN = deadlockPath + @"\game\citadel\resource\localization\citadel_gc_mod_names\citadel_gc_mod_names_english.txt";

    try
    {
        File.Copy(pathModEN, pathModRU, true);
        Console.Clear();
        StarterInfo();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Успешно");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Произошла ошибка: {ex.Message}");
    }
}

string networthPathRU = deadlockPath + @"\game\citadel\resource\localization\citadel_main\citadel_main_russian.txt";

try
{
    // Чтение всех строк из файла с нетворсом
    List<string> lines = new List<string>(File.ReadAllLines(networthPathRU));

    for (int i = 0; i < lines.Count; i++)
    {
        if (lines[i].Contains("тыс."))
        {
            lines[i] = lines[i].Replace("тыс.", "k");
        }
    }

    await File.WriteAllLinesAsync(networthPathRU, lines);
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}

Console.Write(" Закрытие через ");
await Task.Delay(333);
Console.Write("3"); 
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write("2");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write("1");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write(".");
await Task.Delay(333);
Console.Write(".");

Environment.Exit(0);

static void StarterInfo()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("▓█████▄ ▓█████ ▄▄▄      ▓█████▄  ██▓     ▒█████   ▄████▄   ██ ▄█▀\r\n▒██▀ ██▌▓█   ▀▒████▄    ▒██▀ ██▌▓██▒    ▒██▒  ██▒▒██▀ ▀█   ██▄█▒ \r\n░██   █▌▒███  ▒██  ▀█▄  ░██   █▌▒██░    ▒██░  ██▒▒▓█    ▄ ▓███▄░ \r\n░▓█▄   ▌▒▓█  ▄░██▄▄▄▄██ ░▓█▄   ▌▒██░    ▒██   ██░▒▓▓▄ ▄██▒▓██ █▄ \r\n░▒████▓ ░▒████▒▓█   ▓██▒░▒████▓ ░██████▒░ ████▓▒░▒ ▓███▀ ░▒██▒ █▄\r\n ▒▒▓  ▒ ░░ ▒░ ░▒▒   ▓▒█░ ▒▒▓  ▒ ░ ▒░▓  ░░ ▒░▒░▒░ ░ ░▒ ▒  ░▒ ▒▒ ▓▒\r\n ░ ▒  ▒  ░ ░  ░ ▒   ▒▒ ░ ░ ▒  ▒ ░ ░ ▒  ░  ░ ▒ ▒░   ░  ▒   ░ ░▒ ▒░\r\n ░ ░  ░    ░    ░   ▒    ░ ░  ░   ░ ░   ░ ░ ░ ▒  ░        ░ ░░ ░ \r\n   ░       ░  ░     ░  ░   ░        ░  ░    ░ ░  ░ ░      ░  ░   \r\n ░                       ░                       ░               ");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("  _  _      _  __   ___           _ _    _      _____                 _      _             ___   __  \r\n | || |__ _| |/ _| | __|_ _  __ _| (_)__| |_   |_   _| _ __ _ _ _  __| |__ _| |_ ___  __ _|_  ) /  \\ \r\n | __ / _` | |  _| | _|| ' \\/ _` | | (_-< ' \\    | || '_/ _` | ' \\(_-< / _` |  _/ -_) \\ V // / | () |\r\n |_||_\\__,_|_|_|   |___|_||_\\__, |_|_/__/_||_|   |_||_| \\__,_|_||_/__/_\\__,_|\\__\\___|  \\_//___(_)__/ \r\n                            |___/                                                                    ");
    Console.WriteLine(" twitch.tv/megicullla \n");
    Console.ResetColor();
}

static void ActionChoose()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("1. Перевести героев\n" +
        "2. Перевести предметы\n" +
        "3. Перевести все\n" +
        "Введите число (1-3): ");
    Console.ResetColor();
}

static async Task ProgressBar()
{
    await AnsiConsole.Progress().StartAsync(async ctx =>
    {

        var gettingReadyTask = ctx.AddTask("[magenta] Перевод[/]");

        while (!ctx.IsFinished)
        {
            await Task.Delay(300);
            gettingReadyTask.Increment(10.5);

        }
    });
}