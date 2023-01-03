using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
			var rows = original.GetLength(0);
			var columns = original.GetLength(1);
			var result = new double[rows, columns];
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < columns; j++)
				{
					result[i, j] = GetMedian(GetWindow(i, j, original));
				}
			return result;
		}

		public static double GetMedian(double[] window)
		{
			double median = 0;
			var array = window;
			array = array.OrderBy(x => x).ToArray();
			median = (array[(array.Length) / 2] + array[(array.Length - 1) / 2]) / 2;
			return median;
		}

		public static double[] GetWindow(int i, int j, double[,] original)
		{
			var rows = original.GetLength(0);
			var columns = original.GetLength(1);
			var windowList = new List<double>();
			for (int y = -1; y < 2; y++)
				for (int x = -1; x < 2; x++)
				{
					if ((0 <= x + i) && (x + i <= rows - 1) && (0 <= y + j) && (y + j <= columns - 1))
						windowList.Add(original[x + i, y + j]);
				}
			var window = new double[windowList.Count];
			for (int k = 0; k < windowList.Count; k++)
				window[k] = windowList[k];
			return window;
		}
	}
}