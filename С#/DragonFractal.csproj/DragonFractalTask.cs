// В этом пространстве имен содержатся средства для работы с изображениями. 
// Чтобы оно стало доступно, в проект был подключен Reference на сборку System.Drawing.dll
using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			double x = 1;
			double y = 0;
			double temp = 0;
			pixels.SetPixel(x, y);
			var random = new Random(seed);
			for(int i = 1; i < 123; i++)
            {
				var nextNumber = random.Next(1);
				if (nextNumber % 2 == 0)
                {
					temp = x;
					x = TransformationOneX(x, y);
					y = TransformationOneY(temp, y);
				}
				else
                {
					temp = x;
					x = TransformationTwoX(x, y);
					y = TransformationTwoY(temp, y);
				}
				pixels.SetPixel(x, y);
			}
		}

		public static double TransformationOneX(double x, double y)
        {
			double quarterPi = Math.PI / 4;
			double newX = (x * Math.Cos(quarterPi) - y * Math.Sin(quarterPi)) / Math.Sqrt(2);
			return newX;
        }

		public static double TransformationOneY(double x, double y)
		{
			double quarterfPi = Math.PI / 4;
			double newY = (x * Math.Cos(quarterfPi) + y * Math.Sin(quarterfPi)) / Math.Sqrt(2);
			return newY;
		}
		public static double TransformationTwoX(double x, double y)
		{
			double minusQuarterPi = Math.PI * 3 / 4;
			double newX = (x * Math.Cos(minusQuarterPi) - y * Math.Sin(minusQuarterPi)) / Math.Sqrt(2) + 1;
			return newX;
		}

		public static double TransformationTwoY(double x, double y)
		{
			double minusQuarterPi = Math.PI * 3 / 4;
			double newY = (x * Math.Cos(minusQuarterPi) + y * Math.Sin(minusQuarterPi)) / Math.Sqrt(2);
			return newY;
		}
	}
}