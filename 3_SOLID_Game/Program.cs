using _3_SOLID_Game;
using System;

namespace _3_SOLID_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Тест Логики Игры ===");

            IGameConfig config = new DefaultGameConfig();
            GameEngine engine = new GameEngine(config);

            engine.StartNewGame();
            Console.WriteLine($"Загадано число от {config.MinNumber} до {config.MaxNumber}. У вас {config.MaxAttempts} попыток.");

            while (engine.IsGameActive)
            {
                Console.Write("Введите число: ");
                if (int.TryParse(Console.ReadLine(), out int userMove))
                {
                    GuessResult result = engine.ProcessAttempt(userMove);

                    if (result == GuessResult.Win)
                    {
                        Console.WriteLine($"Поздравляем! Вы угадали за {engine.CurrentAttempts} попыток!");
                    }
                    else if (result == GuessResult.GameOver)
                    {
                        Console.WriteLine("Попытки закончились. Вы проиграли.");
                    }
                    else if (result == GuessResult.Greater)
                    {
                        Console.WriteLine("Загаданное число БОЛЬШЕ.");
                    }
                    else if (result == GuessResult.Less)
                    {
                        Console.WriteLine("Загаданное число МЕНЬШЕ.");
                    }
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите корректное число.");
                }
            }

            Console.ReadLine();
        }
    }
}
