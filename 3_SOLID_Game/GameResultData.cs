using System;

namespace _3_SOLID_Game
{
    public class GameResultData
    {
        public DateTime DateTime { get; set; }
        public string Difficulty { get; set; }
        public bool IsWin { get; set; }
        public int AttemptsUsed { get; set; }
    }
}
