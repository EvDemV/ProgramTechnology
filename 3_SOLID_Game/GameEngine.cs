using System;

namespace _3_SOLID_Game
{
    public class GameEngine
    {
        private readonly IGameConfig _config;
        private int _secretNumber;
        private int _currentAttempts;
        private bool _isGameActive;

        public int CurrentAttempts => _currentAttempts;
        public bool IsGameActive => _isGameActive;

        public GameEngine(IGameConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _isGameActive = false;
        }

        // Старт новой игры
        public void StartNewGame()
        {
            Random random = new Random();
            _secretNumber = random.Next(_config.MinNumber, _config.MaxNumber + 1);
            _currentAttempts = 0;
            _isGameActive = true;
        }

        // Обработка попытки игрока
        public GuessResult ProcessAttempt(int userNumber)
        {
            if (!_isGameActive)
            {
                throw new InvalidOperationException("Игра еще не началась или уже завершена.");
            }

            _currentAttempts++;

            if (userNumber == _secretNumber)
            {
                _isGameActive = false;
                return GuessResult.Win;
            }

            if (_currentAttempts >= _config.MaxAttempts)
            {
                _isGameActive = false;
                return GuessResult.GameOver;
            }

            return userNumber < _secretNumber ? GuessResult.Greater : GuessResult.Less;
        }
    }

    // Перечисление для ответов движка (чтобы не возвращать «магические» строки)
    public enum GuessResult
    {
        Win,
        GameOver,
        Greater, // Загаданное число больше
        Less     // Загаданное число меньше
    }
}
