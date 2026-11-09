using System;
using System.Collections.Generic;
using System.Text;

namespace _3_SOLID_Game
{
    public class DefaultGameConfig : IGameConfig
    {
        public int MinNumber => 1;
        public int MaxNumber => 100;
        public int MaxAttempts => 10;
        public string DifficultyName => "Средний";
    }
}
