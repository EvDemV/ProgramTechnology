namespace _3_SOLID_Game
{
    public interface IResultStorage
    {
        void SaveResult(GameResultData result);
        string GetAllStatistics();
    }
}
