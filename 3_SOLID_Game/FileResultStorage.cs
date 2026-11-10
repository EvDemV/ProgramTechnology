using System;
using System.IO;

namespace _3_SOLID_Game
{
    public class FileResultStorage : IResultStorage
    {
        private readonly string _filePath;

        public FileResultStorage(string filePath = "statistics.txt")
        {
            _filePath = filePath;
        }

        public void SaveResult(GameResultData result)
        {
            string status = result.IsWin ? "Победа" : "Поражение";
            string logLine = $"[{result.DateTime:dd.MM.yyyy HH:mm:ss}] " +
                             $"Сложность: {result.Difficulty} | " +
                             $"Результат: {status} | " +
                             $"Попыток использовано: {result.AttemptsUsed}";

            // Дописываем строку в файл (если файла нет, .NET создаст его автоматически)
            File.AppendAllText(_filePath, logLine + Environment.NewLine);
        }
    }
}
