using System;

namespace _3_SOLID_Game
{
    class Program
    {
        private static IGameConfig _selectedConfig;
        private static IResultStorage _storage;

        static void Main(string[] args)
        {
            _storage = new FileResultStorage();

            Console.WriteLine("НАСТРОЙКА ИГРЫ");
            Console.WriteLine("Выберите уровень сложности:");
            Console.WriteLine("1 — Легкий (числа 1-10, 5 попыток)");
            Console.WriteLine("2 — Средний (числа 1-100, 10 попыток)");
            Console.WriteLine("3 — Сложный (числа 1-500, 7 попыток)");
            Console.Write("Ваш выбор: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _selectedConfig = new EasyGameConfig();
                    break;
                case "3":
                    _selectedConfig = new HardGameConfig();
                    break;
                default:
                    _selectedConfig = new DefaultGameConfig();
                    break;
            }

            Console.Clear();

            GameEngine engine = new GameEngine(_selectedConfig);

            // Подписываемся на события движка игры
            engine.OnGameStarted += DisplayGameStarted;
            engine.OnRoundProcessed += DisplayRoundResult;
            engine.OnGameFinished += DisplayGameFinished;

           engine.StartNewGame();

            // Основной цикл ввода
            while (engine.IsGameActive)
            {
                Console.Write("Введите ваше число: ");
                if (int.TryParse(Console.ReadLine(), out int userMove))
                {
                    engine.ProcessAttempt(userMove);
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите корректное целое число.");
                }
            }

            Console.WriteLine("\nНажмите Enter для выхода...");
            Console.ReadLine();
        }

        static void DisplayGameStarted(int min, int max, int maxAttempts, string difficultyName)
        {
            Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В ИГРУ 'УГАДАЙ ЧИСЛО'");
            Console.WriteLine($"Выбранный уровень сложности: {difficultyName}");
            Console.WriteLine($"Я загадал число от {min} до {max}.");
            Console.WriteLine($"У вас есть {maxAttempts} попыток. Удачи!");
            Console.WriteLine("---------------------------------------------");
        }

        static void DisplayRoundResult(string hint, int attemptsLeft)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Подсказка: Загаданное число {hint} вашего.");
            Console.ResetColor();
            Console.WriteLine($"Осталось попыток: {attemptsLeft}");
            Console.WriteLine("---------------------------------------------");
        }

        static void DisplayGameFinished(bool isWin, int secretNumber, int attemptsUsed)
        {
            Console.WriteLine("---------------------------------------------");
            if (isWin)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"ПОЗДРАВЛЯЕМ! Вы угадали число за {attemptsUsed} попыток и победили!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ИГРА ОКОНЧЕНА. Попытки закончились.");
                Console.WriteLine($"Было загадано число: {secretNumber}");
            }
            Console.ResetColor();

            var resultData = new GameResultData
            {
                DateTime = DateTime.Now,
                Difficulty = _selectedConfig.DifficultyName,
                IsWin = isWin,
                AttemptsUsed = attemptsUsed
            };

            _storage.SaveResult(resultData);
            Console.WriteLine("Результат игры успешно сохранен в статистику.");
        }
    }
}
