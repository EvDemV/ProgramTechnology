namespace _3_SOLID_Game
{
    public class HardGameConfig : IGameConfig
    {
        public int MinNumber => 1;
        public int MaxNumber => 500;
        public int MaxAttempts => 7;
        public string DifficultyName => "Сложный";
    }
}
