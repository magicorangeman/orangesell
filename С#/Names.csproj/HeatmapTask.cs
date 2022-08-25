using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var days = new string[30];
            var months = new string[12] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
            var intensity = new double[30, 12];

            for (int i = 0; i < days.Length; i++)
                days[i] = (i + 2).ToString();

            foreach (var name in names)
                if (name.BirthDate.Day != 1)  intensity[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;

            return new HeatmapData(
            "Карта интенсивности рождаемости",
            intensity,
            days,
            months) ;
        }
    }
}