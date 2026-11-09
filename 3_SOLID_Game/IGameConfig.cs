namespace _3_SOLID_Game
{
    // Интерфейс только для чтения настроек
    public interface IGameConfig
    {
        int MinNumber { get; }
        int MaxNumber { get; }
        int MaxAttempts { get; }
        string DifficultyName { get; }
    }

    // Интерфейс для настройки изменяемой конфигурации (если пользователь захочет ввести свои значения)
    public interface IWritableGameConfig : IGameConfig
    {
        void SetupCustomConfig(int min, int max, int attempts);
    }
}
