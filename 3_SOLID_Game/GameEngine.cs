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

        // 1. Объявляем делегаты
        public delegate void GameStartedHandler(int min, int max, int maxAttempts);
        public delegate void RoundProcessedHandler(string hint, int attemptsLeft);
        public delegate void GameFinishedHandler(bool isWin, int secretNumber);

        // 2. Объявляем события на основе этих делегатов
        public event GameStartedHandler OnGameStarted;
        public event RoundProcessedHandler OnRoundProcessed;
        public event GameFinishedHandler OnGameFinished;

        public GameEngine(IGameConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _isGameActive = false;
        }

        public void StartNewGame()
        {
            Random random = new Random();
            _secretNumber = random.Next(_config.MinNumber, _config.MaxNumber + 1);
            _currentAttempts = 0;
            _isGameActive = true;

            // Вызываем событие: "Игра началась!"
            OnGameStarted?.Invoke(_config.MinNumber, _config.MaxNumber, _config.MaxAttempts);
        }

        public void ProcessAttempt(int userNumber)
        {
            if (!_isGameActive) return;

            _currentAttempts++;

            if (userNumber == _secretNumber)
            {
                _isGameActive = false;
                // Вызываем событие: "Победа!"
                OnGameFinished?.Invoke(true, _secretNumber);
                return;
            }

            if (_currentAttempts >= _config.MaxAttempts)
            {
                _isGameActive = false;
                // Вызываем событие: "Проигрыш!"
                OnGameFinished?.Invoke(false, _secretNumber);
                return;
            }

            // Если игра продолжается, генерируем подсказку и вызываем событие раунда
            string hint = userNumber < _secretNumber ? "БОЛЬШЕ" : "МЕНЬШЕ";
            int attemptsLeft = _config.MaxAttempts - _currentAttempts;

            OnRoundProcessed?.Invoke(hint, attemptsLeft);
        }
    }
}
