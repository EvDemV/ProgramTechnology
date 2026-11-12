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
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=============================================");
                Console.WriteLine("         ИГРА «УГАДАЙ ЧИСЛО» (SOLID)         ");
                Console.WriteLine("=============================================");
                Console.WriteLine("1 — Начать новую игру");
                Console.WriteLine("2 — Посмотреть статистику игр");
                Console.WriteLine("0 — Выход");
                Console.Write("\nВыберите действие: ");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        StartGameSession();
                        break;
                    case "2":
                        ShowStatistics();
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите Enter и попробуйте снова.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void StartGameSession()
        {
            Console.Clear();
            Console.WriteLine("ВЫБОР УРОВНЯ СЛОЖНОСТИ");
            Console.WriteLine("1 — Легкий (числа 1–10, 5 попыток)");
            Console.WriteLine("2 — Средний (числа 1–100, 10 попыток)");
            Console.WriteLine("3 — Сложный (числа 1–500, 7 попыток)");
            Console.WriteLine("4 — Пользовательский (свои настройки)");
            Console.Write("\nВаш выбор: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _selectedConfig = new EasyGameConfig();
                    break;
                case "3":
                    _selectedConfig = new HardGameConfig();
                    break;
                case "4":
                    _selectedConfig = CreateCustomConfiguration();
                    break;
                default:
                    _selectedConfig = new DefaultGameConfig();
                    break;
            }

            Console.Clear();

            GameEngine engine = new GameEngine(_selectedConfig);

            engine.OnGameStarted += DisplayGameStarted;
            engine.OnRoundProcessed += DisplayRoundResult;
            engine.OnGameFinished += DisplayGameFinished;

            engine.StartNewGame();

            while (engine.IsGameActive)
            {
                Console.Write("Введите ваше число: ");
                if (int.TryParse(Console.ReadLine(), out int userMove))
                {
                    engine.ProcessAttempt(userMove);
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное целое число.");
                }
            }

            engine.OnGameStarted -= DisplayGameStarted;
            engine.OnRoundProcessed -= DisplayRoundResult;
            engine.OnGameFinished -= DisplayGameFinished;

            Console.WriteLine("\nНажмите Enter, чтобы вернуться в главное меню...");
            Console.ReadLine();
        }

        static IGameConfig CreateCustomConfiguration()
        {
            IWritableGameConfig customConfig = new CustomGameConfig();
            while (true)
            {
                try
                {
                    Console.WriteLine("\nНастройка пользовательского режима");
                    Console.Write("Введите минимальное число: ");
                    int min = int.Parse(Console.ReadLine());

                    Console.Write("Введите максимальное число: ");
                    int max = int.Parse(Console.ReadLine());

                    Console.Write("Введите количество попыток: ");
                    int attempts = int.Parse(Console.ReadLine());

                    customConfig.SetupCustomConfig(min, max, attempts);
                    return customConfig;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка настройки: {ex.Message} Попробуйте снова.");
                    Console.ResetColor();
                }
            }
        }

        static void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("СТАТИСТИКА ИГР");
            Console.WriteLine(_storage.GetAllStatistics());
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
        }

        // Методы-обработчики событий
        static void DisplayGameStarted(int min, int max, int maxAttempts, string difficultyName)
        {
            Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В ИГРУ 'УГАДАЙ ЧИСЛО'");
            Console.WriteLine($"Уровень сложности: {difficultyName}");
            Console.WriteLine($"Загадано число от {min} до {max}. Попыток: {maxAttempts}.");
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
                Console.WriteLine("ИГРА ОКОНЧЕНА. Попытки закончились.");
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
            Console.WriteLine("[Система]: Результат сохранен в файл статистики.");
        }
    }
}
