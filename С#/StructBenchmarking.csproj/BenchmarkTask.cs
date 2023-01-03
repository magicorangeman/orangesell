using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            double duration = 0;
            var watch = new Stopwatch();
            task.Run();
            for (int i = 0; i < repetitionCount; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                watch.Start();
                task.Run();
                watch.Stop();
                duration += (double) watch.ElapsedMilliseconds;
                watch.Reset();
            }    
            
            duration = duration / repetitionCount;
            return duration;
		}
	}

    public class StringBuild : ITask
    {
        public void Run()
        {
            var text = new StringBuilder();
            for (int i = 0; i < 10000; i++)
                text.Append('a');
            text.ToString();
        }
    }

    public class StringConstruct : ITask
    {
        public void Run()
        {
            var text = new string('a', 10000);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();
            var stringBuilder = new StringBuild();
            var stringConstructor = new StringConstruct();
            Assert.Less(benchmark.MeasureDurationInMs(stringConstructor, 10000), benchmark.MeasureDurationInMs(stringBuilder, 10000));
        }
    }
}