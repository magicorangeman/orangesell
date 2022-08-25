using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var days = new string[31];
            var births = new double[31];

            for (int i = 0; i < days.Length; i++)
                days[i] = (i+1).ToString();

            foreach(var line in names)
                if (String.Equals(name, line.Name)) births[line.BirthDate.Day - 1]++;

            births[0] = 0; 

            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                days,
                births);
        }
    }
}