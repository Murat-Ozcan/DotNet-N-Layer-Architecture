using System.Diagnostics;
using MRTFramework.CrossCuttingConcern.Performance.Abstract;

namespace MRTFramework.CrossCuttingConcern.Performance.Concrete
{
    public class PerformanceTimer : IPerformance
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public void StartTime()
        {
            _stopwatch.Start();
        }

        public void StopTime()
        {
            _stopwatch.Stop();
        }

        public double ElapsedTime()
        {
            return _stopwatch.Elapsed.TotalSeconds;
        }

    }
}
