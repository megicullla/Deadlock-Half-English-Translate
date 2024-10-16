string deadlockPath = @"C:\Program Files (x86)\Steam\steamapps\common\Deadlock";
bool deadlockExists = true;

do
{
    if (!Directory.Exists(deadlockPath))
    {
        Console.WriteLine($"Ошибка: директория с игрой не корректна ({deadlockPath})");
        deadlockExists = false;

        Console.WriteLine("Введите путь до корневой папки с игрой: \n");
        var path = Console.ReadLine();

        while (path == null)
        {
            Console.WriteLine("Вы ввели пустое значение. Повторите ввод: \n");
            path = Console.ReadLine();
        }

        if (path != null)
        {
            deadlockPath = path;
        }
    }

    deadlockExists = true;
}
while (!deadlockExists);


string pathRU = deadlockPath + @"\game\citadel\resource\localization\citadel_gc\citadel_gc_russian.txt";   
string pathEN = deadlockPath + @"\game\citadel\resource\localization\citadel_gc\citadel_gc_english.txt";
int startLineRU = 0;  // Номер строки, с которой начинаем замену в РУ файле
int startLineEN = 0;  // Номер строки, с которой начинаем замену в АНГЛ файле

try
{
    // Чтение всех строк из файла АНГЛ
    List<string> linesA = new List<string>(File.ReadAllLines(pathEN));

    // Чтение всех строк из файла РУ
    List<string> linesB = new List<string>(File.ReadAllLines(pathRU));

    for (int i = 0; i < linesB.Count; i++)
    {
        if (linesB[i].Contains("// Hero names"))
        {
            startLineRU = i + 2;
        }
    }

    for (int i = 0; i < linesA.Count; i++)
    {
        if (linesA[i].Contains("// Hero names"))
        {
            startLineEN = i + 2;
        }
    }

    // Проверка, что файл A содержит достаточно строк
    if (startLineEN - 1 >= linesA.Count)
    {
        Console.WriteLine("Файл A не содержит столько строк.");
        return;
    }

    // Строки из файла АНГЛ, начиная с указанной строки
    List<string> linesToCopy = linesA.GetRange(startLineEN - 1, linesA.Count - (startLineEN - 1));

    // Вставка или замена строк в файле РУ
    for (int i = 0; i < linesToCopy.Count; i++)
    {
        int indexB = startLineRU - 1 + i;
        if (indexB < linesB.Count)
        {
            // Если строка в файле РУ существует, заменяем её
            linesB[indexB] = linesToCopy[i];
        }
        else
        {
            // Если строка в файле РУ не существует, добавляем новую строку
            linesB.Add(linesToCopy[i]);
        }
    }

    linesB.RemoveAt(linesB.Count - 1);

    // Запись изменений обратно в файл РУ
    File.WriteAllLines(pathRU, linesB);
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
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

    File.WriteAllLines(networthPathRU, lines);
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}

Console.WriteLine("Были изменены: название героев, название предметов, буквы нетворса");

