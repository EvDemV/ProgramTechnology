namespace _3_SOLID_Game
{
    public class EasyGameConfig : IGameConfig
    {
        public int MinNumber => 1;
        public int MaxNumber => 10;
        public int MaxAttempts => 5;
        public string DifficultyName => "Легкий";
    }
}
