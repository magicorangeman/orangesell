using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var rows = original.GetLength(0);
			var columns = original.GetLength(1);
			var result = new double[rows, columns];
			var threshold = GetThresholdParameter(original, whitePixelsFraction);
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < columns; j++)
				{
					if (original[i, j] >= threshold) result[i, j] = 1;
					else result[i, j] = 0;
				}
			return result;
		}

		public static double GetThresholdParameter(double[,] original, double whitePixelsFraction)
		{
			int threshold = 0;
			var rows = original.GetLength(0);
			var columns = original.GetLength(1);
			var array = new double[rows * columns];
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < columns; j++)
					array[i * columns + j] = original[i, j];
			array = array.OrderByDescending(x => x).ToArray();
			threshold = (int)((array.Length) * whitePixelsFraction);
			if (whitePixelsFraction == 1) return 0;
			if (whitePixelsFraction == 0) return 1000;
			if (array.Length == 1) return 1000;
			if (threshold == 0) return 1000;
			return array[threshold - 1];
		}
	}
}
