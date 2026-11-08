using System;

namespace _3_SOLID_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameConfig config = new DefaultGameConfig();
            GameEngine engine = new GameEngine(config);

            // 3. Подписываемся на события движка игры с помощью оператора +=
            engine.OnGameStarted += DisplayGameStarted;
            engine.OnRoundProcessed += DisplayRoundResult;
            engine.OnGameFinished += DisplayGameFinished;

            // Запускаем игру
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

        // Методы-обработчики, которые реагируют на события:

        static void DisplayGameStarted(int min, int max, int maxAttempts)
        {
            Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В ИГРУ 'УГАДАЙ ЧИСЛО'");
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

        static void DisplayGameFinished(bool isWin, int secretNumber)
        {
            Console.WriteLine("---------------------------------------------");
            if (isWin)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ПОЗДРАВЛЯЕМ! Вы угадали число и победили!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ИГРА ОКОНЧЕНА. Попытки закончились.");
                Console.WriteLine($"Было загадано число: {secretNumber}");
            }
            Console.ResetColor();
        }
    }
}
