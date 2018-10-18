namespace MRTFramework.CrossCuttingConcern.Performance.Abstract
{
    public interface IPerformance
    {
        void StartTime();
        void StopTime();
        double ElapsedTime();
    }
}
