using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            for (int i = 16; i < 513; i *= 2)
            {
                var classArrayCreator = new ClassArrayCreationTask(i);
                var structArrayCreator = new StructArrayCreationTask(i);
                classesTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(classArrayCreator, repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(structArrayCreator, repetitionsCount)));
            }
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            for (int i = 16; i < 513; i *= 2)
            {
                var methodCallWithClass = new MethodCallWithClassArgumentTask(i);
                var methodCallWithStruct = new MethodCallWithStructArgumentTask(i);
                classesTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(methodCallWithClass, repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i, benchmark.MeasureDurationInMs(methodCallWithStruct, repetitionsCount)));
            }
            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}