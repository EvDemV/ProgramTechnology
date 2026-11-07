using System;
using System.Collections.Generic;
using System.Text;

namespace _3_SOLID_Game
{
    public class DefaultGameConfig : IGameConfig
    {
        public int MinNumber { get; } = 1;
        public int MaxNumber { get; } = 100;
        public int MaxAttempts { get; } = 10;
    }
}
