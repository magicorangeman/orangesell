namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var rows = original.GetLength(0);
			var columns = original.GetLength(1);
			var grayscale = new double[rows, columns];
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < columns; j++)
				{
					grayscale[i, j] = (0.299 * original[i, j].R + 0.587 * original[i, j].G + 0.114 * original[i, j].B) / 255;
				}
			return grayscale;
		}
	}
}