namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			int stepCountX = width - 3;
			int stepCountY = height - 3;
			MoveRight(robot, stepCountX);
			MoveDown(robot, stepCountY);
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
	}
}