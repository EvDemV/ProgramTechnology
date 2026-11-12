using System;

namespace _3_SOLID_Game
{
    public class CustomGameConfig : IWritableGameConfig
    {
        public int MinNumber { get; private set; }
        public int MaxNumber { get; private set; }
        public int MaxAttempts { get; private set; }
        public string DifficultyName => "Пользовательский";

        public void SetupCustomConfig(int min, int max, int attempts)
        {
            if (min >= max)
            {
                throw new ArgumentException("Минимальное число должно быть меньше максимального.");
            }
            if (attempts <= 0)
            {
                throw new ArgumentException("Количество попыток должно быть больше нуля.");
            }

            MinNumber = min;
            MaxNumber = max;
            MaxAttempts = attempts;
        }
    }
}
