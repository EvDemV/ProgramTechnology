using System;
using System.Collections.Generic;
using System.Text;

namespace _3_SOLID_Game
{
    public interface IGameConfig
    {
        int MinNumber { get; }
        int MaxNumber { get; }
        int MaxAttempts { get; }
    }
}
