namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			int stepCountX = MainDirectMaze(width, height);
			int stepCountY = MainDirectMaze(height, width);
			for (int i = SequenceMoves(width, height); !robot.Finished; i++)
			{
				if (i % 2 == 0)	MoveRight(robot, stepCountX);
				else MoveDown(robot, stepCountY);
			}
		}

		public static void MoveRight(Robot robot, int stepCount)
		{
			for (int i = 0; i < stepCount; i++)
				robot.MoveTo(Direction.Right);
		}

		public static void MoveDown(Robot robot, int stepCount)
		{
			for (int i = 0; i < stepCount; i++)
				robot.MoveTo(Direction.Down);
		}
		public static int MainDirectMaze(int x, int y)
		{
			int ratio = (int) (x - 2) / (y - 2);
			if (ratio > 1) return ratio;
			else return 1;
		}
		public static int SequenceMoves(int width, int height)
		{
			if (width > height) return 0;
			else return 1;
		}
	}
}